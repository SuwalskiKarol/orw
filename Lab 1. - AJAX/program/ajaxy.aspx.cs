using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Web.Services;
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.Net;
using System.IO;
using System.Text;

public partial class ajaxy : System.Web.UI.Page
{


    


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            Thread.Sleep(3000);
            int a = Convert.ToInt32(TextBox1.Text);
            int b = Convert.ToInt32(TextBox2.Text);

            int suma = a + b;
            int różnica = a - b;
            int iloczyn = a * b;

            Label4.Text = string.Format("Suma = {0}", suma);
            Label5.Text = string.Format("Różnica = {0}", różnica);
            Label6.Text = string.Format("Iloczyn = {0}", iloczyn);
        }
        catch (Exception)
        {

        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        Label7.Text = "Aktualna godzina: " +
              DateTime.Now.ToLongTimeString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Label8.Text = "Strona uruchomiona o godzinie: " +
              DateTime.Now.ToLongTimeString();

        if (IsPostBack == false)
        {
            
            drpContinent.DataSource = DBhelper.GetContinentList();
            drpContinent.DataTextField = "continentName";
            drpContinent.DataValueField = "continentName";
            drpContinent.DataBind();

            
            drpCountry.DataSource = DBhelper.GetCountriesList(drpContinent.SelectedValue);
            drpCountry.DataTextField = "countryName";
            drpCountry.DataValueField = "countryName";
            drpCountry.DataBind();
        }
    }

    //Using jQuery AJAX
    [System.Web.Services.WebMethod]
    public static string OnContinentChange(string continentName)
    {
        DataTable table = DBhelper.GetCountriesList(continentName.Trim());

        string result = string.Empty;

        foreach (DataRow r in table.Rows)
        {
            result += r["countryName"].ToString() + ";";
        }

        return result;
    }



    [WebMethod]
    public static string GetMessage()
    {
        return "jakaś wiadomość";
    }
}
