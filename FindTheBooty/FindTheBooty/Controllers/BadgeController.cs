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
    //controllers badge creation for users
    public class BadgeController : DataController
    {
        //creates a list of already obtained badges for the current user
        public void checkBadges(Models.GeneratedModels.user session)
        {
            //creates the session variable for the current user
            //Models.GeneratedModels.user session = (Models.GeneratedModels.user)Session["LoggedUser"];
            List<Models.GeneratedModels.user_badge_relation> badgeList = database.user_badge_relation.Where(u => u.user_user_id == session.user_id)
                .OrderBy(x => x.badge_badge_id)
                .ToList();

            //calls methods to check for new badges
            checkRank(badgeList, session);
            checkHunts(badgeList, session);
            checkTreasures(badgeList, session);
        }

        //checks the current rank of the user and awards badges
        public void checkRank(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            long points = database.users.Where(u => u.user_id == session.user_id).First().points;

            if (points == 5)
            {
                if (badgeList.Where(item => item.badge_badge_id == 2).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 3).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 4).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 5).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 6).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 7).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 8).Count() == 0)
                {
                    Models.GeneratedModels.user_badge_relation temp = new user_badge_relation();
                    temp.user_user_id = session.user_id;
                    temp.badge_badge_id = 8;
                    temp.completed = true.ToString();

                    database.user_badge_relation.Add(temp);
                    database.SaveChanges();
                }
                
                if (badgeList.Where(item => item.badge_badge_id == 14).Count() == 0)
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

        //checks the number of hunts completed by the user and awards new badges
        public void checkHunts(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            int hunts = database.users.Where(u => u.user_id == session.user_id).First().num_hunts;

            if (hunts == 1)
            {
                if (badgeList.Where(item => item.badge_badge_id == 9).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 13).Count() == 0)
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

        //checks the current user's treasures and awards new badges
        public void checkTreasures(List<Models.GeneratedModels.user_badge_relation> badgeList, Models.GeneratedModels.user session)
        {
            int treasures = database.users.Where(u => u.user_id == session.user_id).First().num_treasures;

            if (treasures == 1)
            {
                if (badgeList.Where(item => item.badge_badge_id == 12).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 10).Count() == 0)
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
                if (badgeList.Where(item => item.badge_badge_id == 11).Count() == 0)
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