//using System;
//using System.Collections;
//using System.Collections.Generic;

//namespace ClanWebSite.Services.Models
//{
//    public class TournamentInfo : IComparer, IComparable
//    {
//        public string tag { get; set; }
//        public string type { get; set; }
//        public string status { get; set; }
//        public string name { get; set; }
//        public int capacity { get; set; }
//        public int playerCount { get; set; }
//        public int maxCapacity { get; set; }
//        public int preparationDuration { get; set; }
//        public int duration { get; set; }
//        public int createTime { get; set; }
//        public object startTime { get; set; }
//        public object endTime { get; set; }
//        public string description { get; set; }

//        public DateTime realStartDate { get; set; }

//        public static void SetRealStartDate(List<TournamentInfo> list)
//        {

//            var currentDateTime = DateTime.Now;

//            foreach (var tournamentInfo in list)
//            {
//                if (tournamentInfo.startTime == null)
//                {
//                    tournamentInfo.startTime = tournamentInfo.createTime;
//                }

//                var realStartDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
//                    .AddSeconds(long.Parse(tournamentInfo.startTime.ToString())).ToLocalTime();
                
//                if (tournamentInfo.status.ToLower() == "inprogress")
//                {
//                    while (realStartDate > currentDateTime)
//                    {
//                        realStartDate = realStartDate.AddHours(-1);
//                    }

//                    tournamentInfo.realStartDate = realStartDate;
//                }
//                else
//                {
//                    tournamentInfo.realStartDate = realStartDate;
//                }
//            }

//        }
//        public int Compare(object x, object y)
//        {
//            var firstObject = x as TournamentInfo;
//            var secondObject = y as TournamentInfo;

//            return Compare(firstObject, secondObject);
//        }

//        public int CompareTo(object obj)
//        {
//            var firstObject = this;
//            var secondObject = obj as TournamentInfo;

//            return Compare(firstObject, secondObject);
//        }

//        private static int Compare(TournamentInfo firstObject, TournamentInfo secondObject)
//        {
//            if (firstObject.status.ToLower() == "inprogress" && secondObject.status.ToLower() != "inprogress")
//            {
//                return -1;
//            }
//            else
//            {
//                if (firstObject.status.ToLower() == "inprogress" && secondObject.status.ToLower() == "inprogress")
//                {
//                    if (firstObject.maxCapacity > secondObject.maxCapacity)
//                        return -1;
//                    else
//                    {
//                        if (firstObject.maxCapacity < secondObject.maxCapacity)
//                        {
//                            return 1;
//                        }
//                    }
//                }
//                else
//                {
//                    if (firstObject.status.ToLower() != "inprogress" && secondObject.status.ToLower() == "inprogress")
//                        return 1;
//                }
//            }

//            if (firstObject.status == "inPreparation" && secondObject.status != "inPreparation")
//            {
//                return -1;
//            }
//            else
//            {
//                if (firstObject.status == "inPreparation" && secondObject.status == "inPreparation")
//                {
//                    return 0;
//                }
//                else
//                {
//                    if (firstObject.status != "inPreparation" && secondObject.status == "inPreparation")
//                        return 2;
//                }
//            }


//            if (firstObject.startTime == null)
//            {
//                firstObject.startTime = firstObject.createTime;
//            }

//            if (secondObject.startTime == null)
//            {
//                secondObject.startTime = secondObject.createTime;
//            }

//            var datetimeFirst = new DateTime(1970, 1, 1, 0, 0, 0, 0)
//                .AddSeconds(long.Parse(firstObject.startTime.ToString())).ToLocalTime();

//            var datetimeSecond = new DateTime(1970, 1, 1, 0, 0, 0, 0)
//                .AddSeconds(long.Parse(secondObject.startTime.ToString())).ToLocalTime();



//            return datetimeSecond.CompareTo(datetimeFirst);
//        }
//    }
//}