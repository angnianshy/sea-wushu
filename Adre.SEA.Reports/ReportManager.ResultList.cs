using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adre.SEA.Database;
using Telerik.Reporting;

namespace Adre.SEA.Reports
{
    public static partial class ReportManager
    {
        public static InstanceReportSource GenerateResultListOverall(Guid matchId)
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

                //angns
                var eventGender = @event?.Gender ?? "N/A";

                var reportSource = LoadInstanceReportSourceFromFile("ResultList.Overall.trdp");

                AddBasicReportParameters(reportSource);

                reportSource.Parameters.Add("EventName", eventName);
                reportSource.Parameters.Add("MatchPhase", matchPhase);
                reportSource.Parameters.Add("MatchVenue", matchVenue);
                reportSource.Parameters.Add("MatchRemark", matchRemark);
                reportSource.Parameters.Add("MatchDate", matchDate);
                reportSource.Parameters.Add("MatchStart", matchStart);

                //angns
                reportSource.Parameters.Add("Gender", eventGender);

                var dataTable = new DataTable();

                dataTable.Columns.Add("Seq");
                dataTable.Columns.Add("Cont");
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Time");
                dataTable.Columns.Add("Remark");
                dataTable.Columns.Add("Medal");  //angns

                var matchesOnSameEvent = ctx.Matches.Where(m => m.Event.Id == @event.Id);

                foreach (var matchOnSameEvent in matchesOnSameEvent)
                {
                    var result = matchOnSameEvent.Result;
                    var athletes = matchOnSameEvent.Athletes.OrderBy(a => a.PreferredName);
                    var athleteContingent = athletes.First()?.Contingent.Code ?? "N/A";
                    var athleteName = athletes.First()?.PreferredName ?? "N/A";

                    dataTable.Rows.Add(
                        matchOnSameEvent.MatchNo,
                        athleteContingent,
                        athleteName,
                        result?.FinalScore,
                        result?.Remarks,
                        result?.Medal   //angns
                    );
                }

                var dataSet = new DataSet();

                dataSet.Tables.Add(dataTable);
                var objectDataSource = new ObjectDataSource { DataSource = dataSet };
                ((Table)((Report)reportSource.ReportDocument).Items.Find("tableResultList", true).First()).DataSource = objectDataSource;

                return reportSource;
            }
        }

        public static InstanceReportSource GenerateResultDetailList(Guid matchId)
        {
            using (var ctx = new ASEAContext())
            {
                var match = ctx.Matches.FirstOrDefault(m => m.Id == matchId);
                var @event = match?.Event;
                var result = ctx.Result?.FirstOrDefault(m => m.Match.Id == match.Id);

                var reportSource = LoadInstanceReportSourceFromFile("ResultDetailList.trdp");

                AddBasicReportParameters(reportSource);

                var dataTable = new DataTable();

                dataTable.Columns.Add("ContingentA");
                dataTable.Columns.Add("ContingentB");
                dataTable.Columns.Add("PlayersA");
                dataTable.Columns.Add("PlayersB");
                dataTable.Columns.Add("Reserve1A");
                dataTable.Columns.Add("Reserve2A");
                dataTable.Columns.Add("Reserve1B");
                dataTable.Columns.Add("Reserve2B");
                dataTable.Columns.Add("Point1A");
                dataTable.Columns.Add("Point2A");
                dataTable.Columns.Add("Point3A");
                dataTable.Columns.Add("Point1B");
                dataTable.Columns.Add("Point2B");
                dataTable.Columns.Add("Point3B");
                dataTable.Columns.Add("ScoreA");
                dataTable.Columns.Add("ScoreB");
                dataTable.Columns.Add("TeamWin");
                dataTable.Columns.Add("TeamLose");
                dataTable.Columns.Add("RoundDifference");
                dataTable.Columns.Add("TotalPointDifference");
                dataTable.Columns.Add("GameDuration");

                var dataSet = new DataSet();

                dataSet.Tables.Add(dataTable);
                var objectDataSource = new ObjectDataSource { DataSource = dataSet };

                return reportSource;
            }
        }

        public static InstanceReportSource GenerateResultDetailListTeamEvent(Guid matchId)
        {
            using (var ctx = new ASEAContext())
            {
                var match = ctx.Matches.FirstOrDefault(m => m.Id == matchId);
                var @event = match?.Event;
                var result = ctx.Result?.FirstOrDefault(m => m.Match.Id == match.Id);

                var eventName = @event?.Name ?? "N/A";
                var matchVenue = match?.Venue ?? "N/A";
                var matchRemark = match?.Remarks ?? "N/A";
                var matchStart = match?.DateTimeStart.ToString("hh:mm tt") ?? "N/A";
                var matchNo = match?.MatchNo.ToString() ?? "N/A";
                var matchDate = match?.DateTimeStart.ToString("dd/MM/yyyy");

                var reportSource = LoadInstanceReportSourceFromFile("ResultDetailListTeamEvent.trdp");

                AddBasicReportParameters(reportSource);
                reportSource.Parameters.Add("EventName", eventName);
                reportSource.Parameters.Add("MatchVenue", matchVenue);
                reportSource.Parameters.Add("MatchRemark", matchRemark);
                reportSource.Parameters.Add("MatchStart", matchStart);

                var dataTable = new DataTable();

                dataTable.Columns.Add("ContingentA");
                dataTable.Columns.Add("ContingentB");
                dataTable.Columns.Add("PlayersA");
                dataTable.Columns.Add("PlayersB");
                dataTable.Columns.Add("Reserve1A");
                dataTable.Columns.Add("Reserve2A");
                dataTable.Columns.Add("Reserve1B");
                dataTable.Columns.Add("Reserve2B");
                dataTable.Columns.Add("Point1A");
                dataTable.Columns.Add("Point2A");
                dataTable.Columns.Add("Point3A");
                dataTable.Columns.Add("Point1B");
                dataTable.Columns.Add("Point2B");
                dataTable.Columns.Add("Point3B");
                dataTable.Columns.Add("ScoreA");
                dataTable.Columns.Add("ScoreB");
                dataTable.Columns.Add("TeamWin");
                dataTable.Columns.Add("TeamLose");
                dataTable.Columns.Add("RoundDifference");
                dataTable.Columns.Add("TotalPointDifference");
                dataTable.Columns.Add("GameDuration");

                dataTable.Rows.Add();

                var dataSet = new DataSet();

                dataSet.Tables.Add(dataTable);
                var objectDataSource = new ObjectDataSource { DataSource = dataSet };

                return reportSource;
            }
        }

    }
}
