using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;

namespace Time_Tracker.Other
{
    public class Tracker
    {
        private readonly ApplicationContext db;

        public Tracker(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<string> StartAsync(User user)
        {
            #region Initialize
            var day = Convert.ToString(DateTime.Now.Date)[..^8];
            var nowDateTime = DateTime.Now.TimeOfDay;
            var now = Convert.ToString(nowDateTime)[..^8];
            var expEndString = Convert.ToString(nowDateTime
                .Add(TimeSpan.Parse(user.Post.ScheduleHours)))[..^8];
            #endregion

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                await CreateNewAsync(user);
                return null;
            }

            var timeToday = await db.Times.Where(x => x.User == user).LastAsync(x => x.Date == day);
            // i`m not sure, which would be correct and faster. gues it`s the first request.
            //var timeToday = user.Time.Find(x => x.Date == dayString);
            if (timeToday == null)
            {
                await CreateNewAsync(user);
                return null;
            }
            else if (timeToday.IsStarted)
            {
                string Error = "You have already started your job";
                return Error;
            }
            else if (timeToday.IsPaused)
            {
                timeToday.IsStarted = true;
                timeToday.IsPaused = false;
                timeToday.PauseDuration = Convert.ToString(nowDateTime - 
                    TimeSpan.Parse(timeToday.StartPause))[..^8];
                timeToday.StartPause = null;
                return null;
            }
            else //timeToday.IsFinished
            {
                await CreateNewAsync(user, timeToday.Session);
                return null;
            }
        }

        public async Task<string> PauseAsync(User user)
        {
            var day = Convert.ToString(DateTime.Now.Date)[..^8];

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                string Error = "You haven't started your job yet";
                return Error;
            }

            var timeToday = await db.Times.Where(x => x.User == user).LastAsync(x => x.Date == day);
            if (timeToday == null)
            {
                string Error = "You haven't started your job yet";
                return Error;
            }
            else if (timeToday.IsStarted)
            {
                var now = DateTime.Now.TimeOfDay;
                var start = TimeSpan.Parse(timeToday.Start);
                timeToday.IsStarted = false;
                timeToday.IsPaused = true;
                timeToday.StartPause = Convert.ToString(now)[..^8];
                timeToday.Duration = Convert.ToString(now - start)[..^8];
                await db.SaveChangesAsync();
                return null;
            }
            else if (timeToday.IsPaused)
            {
                string Error = "You've already gone on break";
                return Error;
            }
            else
            {
                string Error = "You haven't started your job yet";
                return Error;
            }
        }

        public async Task<string> FinishAsync(User user)
        {
            var day = Convert.ToString(DateTime.Now.Date)[..^8];
            var now = DateTime.Now.TimeOfDay;

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                string Error = "You haven't started your job yet";
                return null;
            }
        }

        public async Task CreateNewAsync(User user, int session = 0)
        {
            #region Initialize
            var nowDateTime = DateTime.Now.TimeOfDay;
            var day = Convert.ToString(DateTime.Now.Date)[..^8];
            var now = Convert.ToString(nowDateTime)[..^8];
            var expEndString = Convert.ToString(nowDateTime
                .Add(TimeSpan.Parse(user.Post.ScheduleHours)))[..^8];
            #endregion

            Time time = new Time
            {
                User = user,
                Start = now,
                ExpEnd = expEndString,
                IsStarted = true,
                Date = day,
                Session = 1
            };
            if (session != 0)
            {
                time.Session = session + 1;
            }

            await db.Times.AddAsync(time);
            await db.SaveChangesAsync();
            return;
        }
    }
}