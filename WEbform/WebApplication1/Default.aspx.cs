using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.DB;






namespace WebApplication1
{
    public partial class _Default : Page
    {
        webformEntities1 db = new webformEntities1();

        protected void Page_Load(object sender, EventArgs e)
        {

            loadgridview();
        
        }

        public void loadgridview() {



            var Objperson = new employee();

            gridviewlinq.DataSource = Objperson.loadperson();

            gridviewlinq.DataBind();
           
        
        }
           
   

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

           
            if (ModelState.IsValid)
            {
                var empdb = new Person { 
                 Firstname = txtFirstName.Text,
                 Lastname = txtLastName.Text
                };

                db.People.Add(empdb);
                db.SaveChanges();
                loadgridview();
                txtLastName.Text="";
                txtFirstName.Text = "";
              
            }
        
        }



    }
}