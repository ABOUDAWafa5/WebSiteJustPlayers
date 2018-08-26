using DataBase;
using RoyaleApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleService
{
    public partial class ClanMembers : ServiceBase
    {

        private static System.Timers.Timer aTimer;
        public ClanMembers()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            aTimer = new System.Timers.Timer(21600);

            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.Enabled = true;
            aTimer.Start();

        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var clashRoyaleApi = new ClashRoyaleApi();
            var clanInfo = clashRoyaleApi.GetClanInfo();

            using (var entities = new ClanManagerEntities())
            {
                var existingMembers = entities.Member;

                if (existingMembers.Any())
                {
                    foreach (var member in existingMembers)
                    {
                        if (!clanInfo.members.Any(s => s.tag != member.Tag))
                        {
                            entities.Member.Remove(member);
                        }
                    }
                }

                foreach (var member in clanInfo.members)
                {
                    if (!existingMembers.Any(s => s.Tag == member.tag))
                    {
                        entities.Member.Add(new Member
                        {
                            JoinDate = DateTime.Now,
                            Name = member.name,
                            Tag = member.tag
                        });
                    }
                }

                entities.SaveChanges();
            }
        }

        protected override void OnStop()
        {
            aTimer.Start();
        }
    }
}
