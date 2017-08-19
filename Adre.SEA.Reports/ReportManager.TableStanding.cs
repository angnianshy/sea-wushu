using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adre.SEA.Database;
using Telerik.Reporting;

namespace Adre.SEA.Reports
{
    public static partial class ReportManager
    {
        public static InstanceReportSource GenerateTableStanding(Guid selectedEventId, Guid matchGroupTypeId)
        {
            //using (var ctx = new ASEAContext())
            //{
            //    var eventName = ctx.Events.First(e => e.Id == selectedEventId).Name;
            //    var matches = ctx.Matches.Where(m => m.Event.Id == selectedEventId);
            //    var results = matches.Select(m => m.Results);

            //    var dataTable = new DataTable();

            //    var rowValues = new List<string>();

            //    var totalScores = new Dictionary<string, List<int>>();

            //    foreach (var result in results)
            //    {
            //        List<int> totalScoreByContingent = null;

            //        if (totalScoreByContingent == null)
            //        {
            //            totalScoreByContingent = new List<int>();

            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //        }

            //        totalScoreByContingent = null;

            //        if (totalScoreByContingent == null)
            //        {
            //            totalScoreByContingent = new List<int>();

            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //            totalScoreByContingent.Add(0);
            //        }
            //    }

            //    var contingentInStr = new List<string>();
            //    var totalScoresSorted = totalScores.ToList()
            //        .OrderByDescending(m => m.Value[0])
            //        .ThenByDescending(m => m.Value[1] - m.Value[2])
            //        .ThenByDescending(m => m.Value[3] - m.Value[4]);

            //    foreach (var totalScoreSorted in totalScoresSorted)
            //    {
            //        var joinedContingent = totalScoreSorted.Key;
            //        var safeJoinedCOntingent = joinedContingent.Replace(" ", "");

            //        dataTable.Columns.Add("PointTotal" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SetATotal" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SetBTotal" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SmallPointATotal" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SmallPointBTotal" + safeJoinedCOntingent);

            //        dataTable.Columns.Add("PointDiff" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SetDiff" + safeJoinedCOntingent);
            //        dataTable.Columns.Add("SmallPointDiff" + safeJoinedCOntingent);

            //        var totalScore = totalScoreSorted.Value;

            //        rowValues.Add(totalScore[0].ToString());
            //        rowValues.Add(totalScore[1].ToString());
            //        rowValues.Add(totalScore[2].ToString());
            //        rowValues.Add(totalScore[3].ToString());
            //        rowValues.Add(totalScore[4].ToString());

            //        rowValues.Add(totalScore[0].ToString());
            //        rowValues.Add((totalScore[1] - totalScore[2]).ToString());
            //        rowValues.Add((totalScore[3] - totalScore[4]).ToString());

            //        contingentInStr.Add(joinedContingent);
            //    }

            //    if (dataTable.Columns.Count > 0)
            //        dataTable.Rows.Add(rowValues.ToArray());

            //    var dataSet = new DataSet();

            //    dataSet.Tables.Add(dataTable);

            //    var objectDataSource = new ObjectDataSource { DataSource = dataSet };

            //    var report = new ReportRoundRobin(contingentInStr.ToList(), objectDataSource);

            //    var reportSource = LoadInstanceReportSourceFromClass(report);

            //    AddBasicReportParameters(reportSource);
            //    reportSource.Parameters.Add("EventName", eventName);

            //    return reportSource;
            //}

            return new InstanceReportSource();
        }

    }
}
