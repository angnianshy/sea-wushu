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
        public static InstanceReportSource GenerateStartListOverall(Guid matchId)
        {
            using (var ctx = new ASEAContext())
            {
                var @match = ctx.Matches.FirstOrDefault(m => m.Id == matchId);
                var @event = @match?.Event;

                var eventName = @event?.Name ?? "N/A";
                var matchPhase = @match?.Phase?.Name ?? "N/A";
                var matchVenue = @match?.Venue ?? "N/A";
                var matchRemark = @match?.Remarks ?? "N/A";
                var matchDate = @match?.DateTimeStart.ToString("dd/MM/yyyy") ?? "N/A";
                var matchStart = @match?.DateTimeStart.ToString("hh:mm tt") ?? "N/A";

                var reportSource = LoadInstanceReportSourceFromFile("StartList.Overall.trdp");

                AddBasicReportParameters(reportSource);

                reportSource.Parameters.Add("EventName", eventName);
                reportSource.Parameters.Add("MatchPhase", matchPhase);
                reportSource.Parameters.Add("MatchVenue", matchVenue);
                reportSource.Parameters.Add("MatchRemark", matchRemark);
                reportSource.Parameters.Add("MatchDate", matchDate);
                reportSource.Parameters.Add("MatchStart", matchStart);

                var dataTable = new DataTable();

                dataTable.Columns.Add("MatchNo");
                dataTable.Columns.Add("AthleteContingent");
                dataTable.Columns.Add("AthleteNames");
                dataTable.Columns.Add("AthleteUCICodes");
                dataTable.Columns.Add("AthleteRemarks"); //angns
                 
                var matchesOnSameEvent = ctx.Matches.Where(m => m.Event.Id == @event.Id);

                foreach (var matchOnSameEvent in matchesOnSameEvent)
                {
                  var athletes = matchOnSameEvent.Athletes;//.OrderBy(a => a.PreferredName); //angns PreferredName
                  var athleteContingent = athletes.First()?.Contingent.Code ?? "N/A";
                  var athleteNames = string.Join("\r\n", athletes.Select(a => a.PreferredName));
                  var athleteRemarks = match?.Remarks ?? "N/A"; //angns     
                  //dataTable.Rows.Add(matchOnSameEvent.MatchNo, athleteContingent, athleteNames);  //angns
                  
                  dataTable.Rows.Add(matchOnSameEvent.MatchNo, athleteContingent, athleteNames, athleteRemarks);
                  dataTable.DefaultView.Sort = "MatchNo ASC";
                  dataTable = dataTable.DefaultView.ToTable();
                }

                var dataSet = new DataSet();

                dataSet.Tables.Add(dataTable);
                var objectDataSource = new ObjectDataSource { DataSource = dataSet };
                ((Table)((Report)reportSource.ReportDocument).Items.Find("tableStartList", true).First()).DataSource = objectDataSource;
                             
                return reportSource;
            }
        }

        public static InstanceReportSource GenerateStartDetailList(Guid matchId)
        {
            using (var ctx = new ASEAContext())
            {
                var match = ctx.Matches.FirstOrDefault(m => m.Id == matchId);
                var @event = match?.Event;
                var eventName = @event?.Name ?? "N/A";
                var matchVenue = match?.Venue ?? "N/A";
                var matchRemark = match?.Remarks ?? "N/A";
                var matchStart = match?.DateTimeStart.ToString("hh:mm tt") ?? "N/A";
                var matchNo = match?.MatchNo.ToString() ?? "N/A";
                var matchDate = match?.DateTimeStart.ToString("dd/MM/yyyy");

                var reportSource = LoadInstanceReportSourceFromFile("StartDetailList.trdp");

                AddBasicReportParameters(reportSource);
                reportSource.Parameters.Add("EventName", eventName);
                reportSource.Parameters.Add("MatchVenue", matchVenue);
                reportSource.Parameters.Add("MatchRemark", matchRemark);
                reportSource.Parameters.Add("MatchStart", matchStart);
                reportSource.Parameters.Add("MatchNo", matchNo);
                reportSource.Parameters.Add("MatchDate", matchDate);

                var dataTable = new DataTable();

                dataTable.Columns.Add("ContingentA");
                dataTable.Columns.Add("ContingentB");
                dataTable.Columns.Add("PlayersA");
                dataTable.Columns.Add("PlayersB");
                dataTable.Columns.Add("Reserve1A");
                dataTable.Columns.Add("Reserve2A");
                dataTable.Columns.Add("Reserve1B");
                dataTable.Columns.Add("Reserve2B");

                dataTable.Rows.Add(
                );

                var dataSet = new DataSet();

                dataSet.Tables.Add(dataTable);
                var objectDataSource = new ObjectDataSource { DataSource = dataSet };
                ((Table)((Report)reportSource.ReportDocument).Items.Find("tablePlayers", true).First()).DataSource =
                    objectDataSource;

                return reportSource;
            }
        }
    }
}
