using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FindTheBooty.Models.GeneratedModels;

namespace FindTheBooty.Controllers
{
    public class BadgeController : DataController
    {
        public void checkBadges()
        {
            Models.GeneratedModels.user session = (Models.GeneratedModels.user)Session["LoggedUser"];
            List<Models.GeneratedModels.user_badge_relation> badgeList = database.user_badge_relation.Where(u => u.user_user_id == session.user_id)
                .OrderBy(x => x.badge_badge_id)
                .ToList();

            checkRank(badgeList, session);
            checkHunts(badgeList, session);
            checkTreasures(badgeList, session);
        }

        public void checkRank(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            long points = database.users.Where(u => u.user_id == session.user_id).First().points;

            if (points == 5)
            {
                var value = badgeList.First(item => item.badge_badge_id == 2);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 2;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 15)
            {
                var value = badgeList.First(item => item.badge_badge_id == 3);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 3;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 25)
            {
                var value = badgeList.First(item => item.badge_badge_id == 4);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 4;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 40)
            {
                var value = badgeList.First(item => item.badge_badge_id == 5);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 5;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 60)
            {
                var value = badgeList.First(item => item.badge_badge_id == 6);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 6;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 75)
            {
                var value = badgeList.First(item => item.badge_badge_id == 7);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 7;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (points == 100)
            {
                var value = badgeList.First(item => item.badge_badge_id == 8);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 8;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }

                value = badgeList.First(item => item.badge_badge_id == 14);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 14;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }
        }

        public void checkHunts(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            int hunts = database.users.Where(u => u.user_id == session.user_id).First().num_hunts;

            if (hunts == 1)
            {
                var value = badgeList.First(item => item.badge_badge_id == 9);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 9;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (hunts == 5)
            {
                var value = badgeList.First(item => item.badge_badge_id == 13);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 13;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }
        }

        public void checkTreasures(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            int treasures = database.users.Where(u => u.user_id == session.user_id).First().num_treasures;

            if (treasures == 1)
            {
                var value = badgeList.First(item => item.badge_badge_id == 12);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 12;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (treasures == 10)
            {
                var value = badgeList.First(item => item.badge_badge_id == 10);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 10;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }

            if (treasures == 20)
            {
                var value = badgeList.First(item => item.badge_badge_id == 11);

                if (value == null)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 11;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
            }
        }
    }
}