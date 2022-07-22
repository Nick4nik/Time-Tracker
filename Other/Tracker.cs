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

            var timeTodayLast = await db.Times.Where(x => x.User == user)
                .LastAsync(x => x.Date == day);
            // i`m not sure, which would be correct and faster. gues it`s the first request.
            //var timeToday = user.Time.Find(x => x.Date == dayString);
            if (timeTodayLast == null)
            {
                await CreateNewAsync(user);
                return null;
            }
            else if (timeTodayLast.IsStarted)
            {
                string Error = "You have already started your job";
                return Error;
            }
            else if (timeTodayLast.IsPaused)
            {
                timeTodayLast.IsStarted = true;
                timeTodayLast.IsPaused = false;
                timeTodayLast.PauseDuration = Convert.ToString(nowDateTime - 
                    TimeSpan.Parse(timeTodayLast.StartPause))[..^8];
                timeTodayLast.EndPause = now;
                timeTodayLast.StartPause = null;

                db.Times.Update(timeTodayLast);
                await db.SaveChangesAsync();
                return null;
            }
            else if (timeTodayLast.IsFinished) // IsFinished
            {
                await CreateNewAsync(user, timeTodayLast.Session);
                return null;
            }
            return null;
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

            var timeToday = await db.Times.Where(x => x.User == user)
                .LastAsync(x => x.Date == day);
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

                db.Times.Update(timeToday);
                await db.SaveChangesAsync();
                return null;
            }
            else if (timeToday.IsPaused)
            {
                string Error = "You've already gone on break";
                return Error;
            }
            else if (timeToday.IsFinished) // IsFinished
            {
                string Error = "You haven't started your job yet";
                return Error;
            }
            return null;
        }

        public async Task<string> FinishAsync(User user)
        {
            var day = Convert.ToString(DateTime.Now.Date)[..^8];
            

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                string Error = "You haven't started your job yet";
                return Error;
            }
            var timeTodayLast = await db.Times.Where(x => x.User == user)
                .LastAsync(x => x.Date == day);
            if (timeTodayLast.IsStarted)
            {
                if (timeTodayLast.PauseDuration == null)
                {
                    await SaveAsync(timeTodayLast, 0);
                    return null;
                }
                await SaveAsync(timeTodayLast, 1);
                return null;
            }
            else if (timeTodayLast.IsPaused)    // it`s part in process
            {
                await SaveAsync(timeTodayLast, 2);
                return null;
            }
            else if (timeTodayLast.IsFinished) // IsFinished
            {
                string Error = "You haven't started your job yet";
                return Error;
            }
            return null;
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
                ExpDuration = expEndString,
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

        public async Task SaveAsync (Time model, int pause)
        {
            var now = DateTime.Now.TimeOfDay;

            if (pause == 0)
            {
                model.Duration = Convert.ToString(now -
                TimeSpan.Parse(model.Start))[..^8];
                
            }
            else if (pause == 1)
            {
                //model.Duration = (now - model.EndPause) + model.Duration;
                model.Duration = Convert.ToString(
                    (now.Subtract(TimeSpan.Parse(model.EndPause)))
                    .Add(TimeSpan.Parse(model.Duration)))[..^8];
            }
            else
            {
                model.PauseDuration = Convert.ToString(
                    now.Subtract(TimeSpan.Parse(model.StartPause)))[..^8];
                model.Duration = model.Duration;
            }

            var isFullDay = model.Duration.CompareTo(
                TimeSpan.Parse(model.ExpDuration));
            if (isFullDay >= 0)
            {
                model.IsFullDay = true;
            }
            else
            {
                model.IsFullDay = false;
            }

            model.End = Convert.ToString(now)[..^8];
            model.IsStarted = false;
            model.IsPaused = false;
            model.IsFinished = true;
            model.ExpDuration = null;
            model.StartPause = null;
            model.EndPause = null;

            db.Times.Update(model);
            await db.SaveChangesAsync();
        }
    }
}