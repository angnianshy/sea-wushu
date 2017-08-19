using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using Telerik.Reporting;

namespace Adre.SEA.Reports
{
    public static partial class ReportManager
    {
        public static void AddBasicReportParameters(InstanceReportSource reportSource)
        {
            var settings = new List<string>
            {
                "ReportMainHeaderLabel",
                "ReportLeftLogoPath",
                "ReportRightLogoPath",
                "ReportRightLogoLabel"
            };

            foreach (var setting in settings) reportSource.Parameters.Add(setting, ConfigurationManager.AppSettings[setting]);
        }

        public static InstanceReportSource LoadInstanceReportSourceFromFile(string path)
        {
            InstanceReportSource result = null;

            var combinedPath = Path.Combine(ConfigurationManager.AppSettings["ReportPath"], path);

            using (var sourceStream = File.OpenRead(combinedPath))
            {
                var reportPackager = new ReportPackager();
                var report = (Report)reportPackager.UnpackageDocument(sourceStream);
                if (report != null) result = new InstanceReportSource { ReportDocument = report };
            }

            return result;
        }

        private static InstanceReportSource LoadInstanceReportSourceFromClass(IReportDocument report)
        {
            var reportSource = new InstanceReportSource { ReportDocument = report };
            return reportSource;
        }

        private static InstanceReportSource LoadInstanceReportSourceFromClass<T>() where T : new()
        {
            var reportSource = new InstanceReportSource { ReportDocument = (IReportDocument)new T() };


            return reportSource;
        }

        public static DataSet GenerateDataSetFromDynamicObject(List<dynamic> dynamicObjects)
        {
            var result = new DataSet();
            var table = new DataTable();

            foreach (var dynamicObject in dynamicObjects)
            {
                var listOfDataToBeAdded = new List<object>();

                var props = dynamicObject.GetType().GetProperties();

                foreach (var prop in props)
                {
                    if (!table.Columns.Contains(prop.Name))
                    {
                        table.Columns.Add(prop.Name);
                    }

                    listOfDataToBeAdded.Add(prop.GetValue(dynamicObject));
                }

                table.Rows.Add(listOfDataToBeAdded.ToArray());
            }

            result.Tables.Add(table);

            return result;
        }

        public static Table FindTable(InstanceReportSource reportSource, string tableName)
        {
            return FindTable(reportSource.ReportDocument, tableName);
        }

        public static Table FindTable(IReportDocument reportDocument, string tableName)
        {
            return (Table) ((Report) reportDocument).Items.Find(tableName, true).First();
        }
    }
}
