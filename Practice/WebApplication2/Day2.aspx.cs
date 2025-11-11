using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Day2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                ddlFruits.Items.Add("apple");
                ddlFruits.Items.Add("banan");
                ddlFruits.Items.Add("qweqe");

                rblColor.Items.Add("red");
                rblColor.Items.Add("green");
                rblColor.Items.Add("blue");

                lstAnimals.Items.Add("cat");
                lstAnimals.Items.Add("dog");
                lstAnimals.Items.Add("bird");
                lstAnimals.Items.Add("sneak");
                
            }
        }

        protected void ddlFruits_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResult.Text = "Fruits: " + ddlFruits.SelectedValue;
        }

        protected void rblColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResult.Text = "Color: " + rblColor.SelectedValue;
        }

        protected void lstAnimals_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = "Anminal: ";
            foreach(var item in lstAnimals.Items){
                var li=(System.Web.UI.WebControls.ListItem)item;
                if(li.Selected){
                    s += li.Value;
                }
            }
            lblResult.Text=s;
        }
    }
}