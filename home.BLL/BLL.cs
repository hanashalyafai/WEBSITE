using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using home.DAL;
namespace home.BLL
{
    public class BLL
    {
        /*
         * title = y.Title,
                     pin = x.pin,
                     state = x.state,
                     time = x.time,
                     name = x.name,
                     listID = x.listID
         * */
        public class HouseApplianceList
        {
            public string title { set; get; }
            public int pin { set; get; }
            public string state { set; get; }
            public DateTime time { set; get; }
            public string name { set; get; }
            public int listID { set; get; }

        }


        static public List<appliances> getAppliances(Guid id)
        {

            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                return entity.appliances.Where(e => e.subID == id).ToList();
            }
        }

        public static List<house> getHouse()
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                return entity.house.ToList();
            }
        }

        public static bool addCommand(Guid inputID, bool isOn, DateTime date, int inputPin)
        {
            try
            {
                using (homeautomationDBEntities entity = new homeautomationDBEntities())
                {
                    entity.list.Add(new list
                    {
                        pin = inputPin,
                        state = isOn,
                        time = date,
                    });
                    entity.SaveChanges();

                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<HouseApplianceList> getList(bool isAll)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                return getHouseApplList(entity,isAll);

                // return null; //entity.list.ToList();
            }
        }

        private static List<HouseApplianceList> getHouseApplList(homeautomationDBEntities entity, bool isAll=false)
        {
            DateTime current =  DateTime.UtcNow.AddHours(2) ;


            var all = (entity.list.Join(entity.appliances, x => x.pin, y => y.pin, (x, y) => new
            {
                pin = x.pin,
                state = x.state,
                time = x.time,
                name = y.name,
                listID = x.ListID,
                applianceID = y.subID
            }).Join(entity.house, x => x.applianceID, y => y.HouseID, (x, y) => new HouseApplianceList
            {
                title = y.Title,
                pin = x.pin,
                state = x.state ? "ON" : "OFF",
                time = x.time,
                name = x.name,
                listID = x.listID
            }));

            if (isAll)
            {
                return all.ToList();
            }
            else
            {
                return all.Where(e => e.time > current).ToList();
            }
           

        }

        public static list getCommand(int id)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                return entity.list.ToList().Find(e => e.ListID == id && e.time > DateTime.UtcNow.AddHours(2));

            }
        }

        // get old  to enable security mode

        public static List<list> getRandomDay()
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                List<list> all = entity.list.ToList(); //.Sort();
                all.Sort();
                ///TODO get a random day !!
                ///
               
                List<list> sub = list.getCommandsInDay(all);

                foreach (list item in sub)
                {
                    //TimeSpan ts = new TimeSpan(DateTime.UtcNow.Year - item.time.Year, DateTime.UtcNow.Month - item.time.Month, DateTime.UtcNow.Day - item.time.Day);
                    item.time = item.time.AddYears(DateTime.UtcNow.Year - item.time.Year);
                    item.time =item.time.AddMonths(DateTime.UtcNow.Month - item.time.Month);
                    item.time = item.time.AddDays(DateTime.UtcNow.Day - item.time.Day);
                   // item.time = item.time.Date + ts;
                }


                return sub; 


            }
        }

        public static void saveUpdate(int listID, bool isOn, DateTime updatedTime)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                list command = entity.list.ToList().Find(e => e.ListID == listID);
                entity.list.Remove(command);
                entity.SaveChanges();
                command.state = isOn;
                command.time = updatedTime;
                entity.list.Add(command);
                entity.SaveChanges();
            }
        }

        public static List<HouseApplianceList> deleteCommand(int id)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                list command = entity.list.ToList().Find(e => e.ListID == id);
                entity.list.Remove(command);
                entity.SaveChanges();
                return getHouseApplList(entity);
            }
        }

        public static List<settings> getSettings()
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                return entity.settings.OrderBy(e => e.name).ToList();
            }
            //return null;
        }

        public static List<settings> UpdateSettings(settings obj)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                entity.settings.Remove(entity.settings.First(s => s.index == obj.index));
                entity.SaveChanges();
                entity.settings.Add(obj);
                entity.SaveChanges();
                return entity.settings.OrderBy(e => e.name).ToList();
            }
        }

        public static void updateAppliancesStatus(bool[,] status)
        {
            using(homeautomationDBEntities entity = new homeautomationDBEntities()){
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        appliances apl = (appliances)entity.appliances.Where(e => e.pin == i * 4 + j);
                        if (apl.state != status[i, j]) // reset! .. different 
                        {
                            entity.appliances.Remove(apl);
                            entity.SaveChanges();
                            apl.state = status[i, j];
                            entity.appliances.Add(apl);
                            entity.SaveChanges();
                        }
                        else // same value 
                        {

                        }
                    }
                }

            }
                
        }

        public static string getUserPassword(string username)
        {
            using (homeautomationDBEntities entity = new homeautomationDBEntities())
            {
                try
                {
                    return entity.users.First(e => e.username.Equals(username)).password.ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }



        }

        public static void addMessage(string inName, string inEmail, string inPhoneNum, string inMsg)
        {
            
           // Guid idd = new Guid();
            try
            {
                using (homeautomationDBEntities entity = new homeautomationDBEntities())
                {
                    entity.emails.Add(new emails
                    {
                        email = inEmail,
                        name = inName,
                        phoneNumber = inPhoneNum,
                        message=inMsg,
                        ID = Guid.NewGuid()
                    });
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
