using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
/// <summary>
/// Summary description for DBhelper
/// </summary>
public class DBhelper
{
	
		public static DataTable GetContinentList()
    {
        DataTable result = null;

        try
        {
            using (SqlConnection con = new SqlConnection(
              ConfigurationManager.ConnectionStrings["countriesDb"].ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select distinct continentName from Countries";

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        result = new DataTable();
                        da.Fill(result);
                    }
                }
            }
        }
        catch (Exception)
        {
            //pokeomn
        }
        return result;
    }

    public static DataTable GetCountriesList(string continentNmame)
    {
        DataTable result = null;
        
        try
        {
            using (SqlConnection con = new SqlConnection(
              ConfigurationManager.ConnectionStrings["countriesDb"].ConnectionString))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select countryName from Countries where continentName = @continent";
                    cmd.Parameters.Add(new SqlParameter("@continent", continentNmame));

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        result = new DataTable();
                        da.Fill(result);
                    }
                }
            }
        }
        catch (Exception)
        {
            //Pokemon exception handling
        }
        return result;
    }
	}
