using Anki.Vector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorControllerShell.Model
{
    public class Vector
    {
        public string Name { get; set; }
        public bool Sleep { get; set; }
        public bool isDocked { get; set; }
        public bool isConnected { get; set; }

        public List<Vector> CreateAllVector()
        {
            List<Vector> listeVector = new List<Vector>();

            var listeStringVector = ConfigurationManager.AppSettings["ListeVector"].ToString().Split(';').ToList();
            if (listeStringVector != null)
            {
                foreach (var vectorName in listeStringVector)
                {
                    listeVector.Add(new Vector() { Name = "Vector-" + vectorName, Sleep = false, isDocked = false, isConnected = false });
                }
            }
            return listeVector;
        }
        public async Task UpdateVectorStatusAsync()
        {
            using (var robot = await Robot.NewConnection(this.Name))
            {
                this.isDocked = robot.Status.IsOnCharger;
                this.Sleep = robot.Status.IsCharging;
            }
        }

        public async Task GoToSleepAsync()
        {
            using (var robot = await Robot.NewConnection(this.Name))
            {
                if (await robot.Control.RequestControl())
                {
                    await robot.Behavior.DriveOnCharger();
                    await robot.Behavior.Sleep();
                    this.isDocked = true;
                    this.Sleep = true;
                }
            }
        }
    }
}
