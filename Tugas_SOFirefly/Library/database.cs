using System;
using System.Data;
using System.Data.SQLite;

namespace TugasSOFirefly.Library
{
    public class database
    {
        public static SQLiteConnection sql_con  = new SQLiteConnection("Data Source=firefly.db;Version=3;");

        public static void InsertKeterangan(params object[] data)
        {
            //sql_con.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO tbl_mainhead (date_start, date_end,kiteria_berhenti,iterasi_end,inp_alpha,inp_beta0,inp_gamma,inp_delta) VALUES (?,?,?,?,?,?,?,?)", sql_con);
            insertSQL.Parameters.Add("@date_start", DbType.String).Value = data[0].ToString();
            insertSQL.Parameters.Add("@date_end", DbType.String).Value = data[1].ToString();
            insertSQL.Parameters.Add("@kiteria_berhenti", DbType.String).Value = data[2].ToString();
            insertSQL.Parameters.Add("@iterasi_end", DbType.Int32).Value = int.Parse(data[3].ToString());
            insertSQL.Parameters.Add("@inp_alpha", DbType.String).Value = data[4].ToString();
            insertSQL.Parameters.Add("@inp_beta0", DbType.String).Value = data[5].ToString();
            insertSQL.Parameters.Add("@inp_gamma", DbType.String).Value = data[6].ToString();
            insertSQL.Parameters.Add("@inp_delta", DbType.String).Value = data[7].ToString();
            try
            {
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //sql_con.Close();
        }

        public static void UpdateKeterangan(params object[] data)
        {
            //sql_con.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("update tbl_mainhead set "+data[2].ToString()+" ='"+data[0].ToString()+"' WHERE id="+int.Parse(data[1].ToString()), sql_con);
            try
            {
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //sql_con.Close();

        }

        public static void InsertAkhir(params object[] data)
        {
            //sql_con.Open();
            SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO tbl_iterasi(id_key,iterasi,min,min_X,min_Y,max,ave) VALUES (?,?,?,?,?,?,?)", sql_con);

            insertSQL.Parameters.Add("@id_key", DbType.Int32).Value = int.Parse(data[0].ToString());
            insertSQL.Parameters.Add("@iterasi", DbType.Int32).Value = int.Parse(data[1].ToString());
            insertSQL.Parameters.Add("@min", DbType.String).Value = data[2].ToString();
            insertSQL.Parameters.Add("@min_X", DbType.String).Value = data[3].ToString();
            insertSQL.Parameters.Add("@min_Y", DbType.String).Value = data[4].ToString();
            insertSQL.Parameters.Add("@max", DbType.String).Value = data[5].ToString();
            insertSQL.Parameters.Add("@ave", DbType.String).Value = data[6].ToString();
            try
            {
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //sql_con.Close();
        }

        public static int GetLastID()
        {
            int datam = 0;
            //sql_con.Open();
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = sql_con;
            cmd.CommandText = "SELECT id FROM tbl_mainhead ORDER BY id DESC LIMIT 1";
            //Assign the data from urls to dr
            SQLiteDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                datam = int.Parse(dr[0].ToString());
            }

            //sql_con.Close();
            return datam;
        }
    }

}
