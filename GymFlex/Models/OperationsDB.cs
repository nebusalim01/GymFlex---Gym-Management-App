using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
namespace GymFlex.Models
{
    public class OperationsDB
    {
        SqlConnection con = new SqlConnection(@"Data Source=LAPTOP-93A161T4\SQLEXPRESS;Initial Catalog=GymFlex;Integrated Security=True");
        public int getMaxRegId()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_MaxReg", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                if (result == DBNull.Value || result == null)
                {
                    return 1;   // first registration
                }

                return Convert.ToInt32(result) + 1;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public int getEmailCount(AdminReg objCls)
        {
            SqlCommand cmd = new SqlCommand("sp_CountEmail", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eml", objCls.Email);

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result == DBNull.Value || result == null)
                return 0;

            return Convert.ToInt32(result);
        }
        public int InsertAdmin(AdminReg objCls, int regId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_AdminReg", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", regId);
                    cmd.Parameters.AddWithValue("@na", objCls.Name);
                    cmd.Parameters.AddWithValue("@ph", objCls.Phone);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public int InsertLogin(AdminReg objCls, int regId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_LoginInsert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", regId);
                    cmd.Parameters.AddWithValue("@eml", objCls.Email);
                    cmd.Parameters.AddWithValue("@pwd", objCls.Password);
                    cmd.Parameters.AddWithValue("@ltype", "Admin");
                    cmd.Parameters.AddWithValue("@dt", DateTime.Now);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows;   // 1 = success
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public int InsertUser(UserReg obj, int regId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("sp_UserReg", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", regId);
                    cmd.Parameters.AddWithValue("@na", obj.Name);
                    cmd.Parameters.AddWithValue("@ag", obj.Age);
                    cmd.Parameters.AddWithValue("@hgt", obj.Height);
                    cmd.Parameters.AddWithValue("@wgt", obj.Weight);
                    cmd.Parameters.AddWithValue("@bmi", obj.BMI);
                    cmd.Parameters.AddWithValue("@gl", obj.Goal);
                    cmd.Parameters.AddWithValue("@actlvl", obj.Activity_Level);

                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    int rows = cmd.ExecuteNonQuery();

                    return rows;   // 1 = success
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public int CheckLoginCount(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("sp_LoginCount", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eml", email);
            cmd.Parameters.AddWithValue("@pwd", password);

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return count;
        }
        public string GetLoginType(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("sp_GetLoginType", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eml", email);
            cmd.Parameters.AddWithValue("@pwd", password);

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result == null || result == DBNull.Value)
                return "Invalid";

            return result.ToString()!;
        }
        public int GetRegId(string email, string password)
        {
            SqlCommand cmd = new SqlCommand("sp_GetRegId", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@eml", email);
            cmd.Parameters.AddWithValue("@pwd", password);

            con.Open();
            object result = cmd.ExecuteScalar();
            con.Close();

            if (result == null || result == DBNull.Value)
                return 0;

            return Convert.ToInt32(result);
        }
        public List<UserListModel> GetAllUsers()
        {
            List<UserListModel> list = new List<UserListModel>();

            SqlCommand cmd = new SqlCommand("sp_GetAllUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                UserListModel u = new UserListModel();

                u.User_Id = Convert.ToInt32(dr["User_Id"]);
                u.Name = dr["Name"].ToString();
                u.Age = Convert.ToInt32(dr["Age"]);
                u.Height = Convert.ToInt32(dr["Height"]);
                u.Weight = Convert.ToInt32(dr["Weight"]);
                u.BMI = Convert.ToSingle(dr["BMI"]);
                u.Goal = dr["Goal"].ToString();
                u.Activity_Level = dr["Activity_Level"].ToString();

                list.Add(u);
            }
            con.Close();
            return list;
        }
        public AdminProfileModel GetAdminProfile(int regId)
        {
            AdminProfileModel obj = new AdminProfileModel();

            SqlCommand cmd = new SqlCommand("sp_GetAdminProfile", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", regId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                obj.AdminId = Convert.ToInt32(dr["Admin_Id"]);
                obj.Name = dr["Name"].ToString();
                obj.Phone = dr["Phone"].ToString();
                obj.Email = dr["Email"].ToString();
                obj.CreatedAt = Convert.ToDateTime(dr["CreatedAt"]);
            }
            con.Close();
            return obj;
        }

    }
}
