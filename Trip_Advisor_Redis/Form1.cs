using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trip_Advisor_Neo4j.DataAccess;
using Trip_Advisor_Neo4j.DomainModel;

namespace Trip_Advisor_Redis
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Trip_Advisor_Neo4j.DataLayer.Connect();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            RedisDataLayer.InitializeCounters();
            RedisDataLayer.SaveTopPlaces();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get top countries


            RedisDataLayer.RefreshPlaceCache();

        }

        private void Take_Click(object sender, EventArgs e)
        {
            try
            {
                List<Place> l = RedisDataLayer.GetTopPlacesByRating();
                string test = string.Empty;
                foreach (Place p in l)
                    test += p.Name + "\n";

                MessageBox.Show(test);

                l = RedisDataLayer.GetTopPlacesByVisitors();

                test = string.Empty;
                foreach (Place p in l)
                    test += p.Name + "\n";

                MessageBox.Show(test);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}

