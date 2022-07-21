using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Time_Tracker.Models;
using Time_Tracker.ViewModels.Times;

namespace Time_Tracker.Other
{
    public class Tracker
    {
        private readonly ApplicationContext db;

        public Tracker(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<TrackerModel> StartAsync(User user)
        {
            #region Initialize
            var dayString = Convert.ToString(DateTime.Now.Date)[..^8];
            var now = DateTime.Now.TimeOfDay;
            var nowString = Convert.ToString(now)[..^8];
            var expEnd = now.Add(TimeSpan.Parse(user.Post.ScheduleHours));
            var expEndString = Convert.ToString(expEnd)[..^8];
            TrackerModel model = new TrackerModel
            {
                user = user,
                IsStarted = true,
            };
            
            #endregion

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                Time time = new Time
                {
                    User = user,
                    Start = nowString,
                    StartPause = now,
                    ExpEnd = expEndString,
                    IsStarted = true,
                    Date = dayString,
                    Session = 1
                };

                await db.Times.AddAsync(time);
                await db.SaveChangesAsync();
                return model;
            }

            var timeToday = user.Time.Find(x => x.Date == dayString);
            if (timeToday == null)
            {
                Time time = new Time
                {
                    User = user,
                    Start = nowString,
                    StartPause = now,
                    ExpEnd = expEndString,
                    IsStarted = true,
                    Date = dayString,
                    Session = 1
                };

                await db.Times.AddAsync(time);
                await db.SaveChangesAsync();
                return model;
            }

            if (timeToday.IsStarted)
            {
                model.IsStarted = true;
                model.Error = "You have already started your job";
                return model;
            }
            else if (timeToday.IsPaused)
            {
                //model.IsStarted = true;
                //model.IsPaused = false;
                //modelToday.IsStarted = true;
                //modelToday.IsPaused = false;
                //modelToday.PauseDuration = Convert.ToString(now - time.StartPause)[..^8];
            }
            else //timeToday.IsFinished
            {
                //
            }

            return model;
        }

        public async Task<TrackerModel> PauseAsync(User user)
        {
            var dayString = Convert.ToString(DateTime.Now.Date)[..^8];
            var now = DateTime.Now.TimeOfDay;
            var nowString = Convert.ToString(now)[..^8];
            TrackerModel model = new TrackerModel
            {
                user = user,
            };

            var timeAny = await db.Times.AnyAsync();
            if (!timeAny)
            {
                model.Error = "You haven't started your job yet";
                return model;
            }

            var timeToday = user.Time.Find(x => x.Date == dayString);
            if (timeToday == null)
            {
                model.Error = "You haven't started your job yet";
                return model;
            }
            else if (timeToday.IsStarted)
            {

            }
            else if (timeToday.IsPaused)
            {
                model.Error = "You've already gone on break";
                return model;
            }
            else
            {
                model.Error = "You haven't started your job yet";
                return model;
            }
            return model;
        }

        public async Task<TrackerModel> FinishAsync(User user)
        {
            TrackerModel model = new TrackerModel();
            return model;
        }
    }
}