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
        public int InsertWorkoutPlan(WorkoutPlanModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_InsertWorkoutPlan", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pname", obj.Plan_Name);
            cmd.Parameters.AddWithValue("@goal", obj.Goal_Type);
            cmd.Parameters.AddWithValue("@level", obj.Level);
            cmd.Parameters.AddWithValue("@days", obj.Duration_Days);

            con.Open();
            int rows = cmd.ExecuteNonQuery();
            con.Close();

            return rows;
        }
        public List<WorkoutPlanModel> GetWorkoutPlans()
        {
            List<WorkoutPlanModel> list = new List<WorkoutPlanModel>();

            SqlCommand cmd = new SqlCommand("sp_GetWorkoutPlans", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                WorkoutPlanModel wp = new WorkoutPlanModel();

                wp.Plan_Id = Convert.ToInt32(dr["Plan_Id"]);
                wp.Plan_Name = dr["Plan_Name"].ToString();
                wp.Duration_Days = Convert.ToInt32(dr["Duration_Days"]);
                wp.Goal_Type = dr["Goal_Type"].ToString();
                wp.Level = dr["Level"].ToString();

                list.Add(wp);
            }
            con.Close();
            return list;
        }
        public int InsertWorkoutDay(WorkoutDayModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_InsertWorkoutDay", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@planId", obj.Plan_Id);
            cmd.Parameters.AddWithValue("@dayNo", obj.Day_Number);
            cmd.Parameters.AddWithValue("@title", obj.Title);
            cmd.Parameters.AddWithValue("@notes", obj.Notes);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
        public List<WorkoutDayModel> GetWorkoutDaysByPlan(int planId)
        {
            List<WorkoutDayModel> list = new List<WorkoutDayModel>();

            SqlCommand cmd = new SqlCommand("sp_GetWorkoutDaysByPlan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@planId", planId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                WorkoutDayModel obj = new WorkoutDayModel();

                obj.Workoutday_Id = Convert.ToInt32(dr["Workoutday_Id"]);
                obj.Plan_Id = Convert.ToInt32(dr["Plan_Id"]);
                obj.Day_Number = Convert.ToInt32(dr["Day_Number"]);
                obj.Title = dr["Title"].ToString();
                obj.Notes = dr["Notes"].ToString();

                list.Add(obj);
            }

            con.Close();
            return list;
        }
        public int InsertWorkoutExercise(WorkoutExerciseModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_InsertWorkoutExercise", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@dayId", obj.Workoutday_Id);
            cmd.Parameters.AddWithValue("@name", obj.Exercise_Name);
            cmd.Parameters.AddWithValue("@sets", obj.Sets);
            cmd.Parameters.AddWithValue("@reps", obj.Reps);
            cmd.Parameters.AddWithValue("@rest", obj.Rest_Seconds);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
        public List<WorkoutExerciseModel> GetExercisesByDay(int dayId)
        {
            List<WorkoutExerciseModel> list = new List<WorkoutExerciseModel>();

            SqlCommand cmd = new SqlCommand("sp_GetExercisesByDay", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dayId", dayId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                WorkoutExerciseModel e = new WorkoutExerciseModel();

                e.Exercise_Id = Convert.ToInt32(dr["Exercise_Id"]);
                e.Workoutday_Id = Convert.ToInt32(dr["Workoutday_Id"]);
                e.Exercise_Name = dr["Exercise_Name"].ToString();
                e.Sets = Convert.ToInt32(dr["Sets"]);
                e.Reps = dr["Reps"].ToString();
                e.Rest_Seconds = Convert.ToInt32(dr["Rest_Seconds"]);

                list.Add(e);
            }

            con.Close();
            return list;
        }
        public int InsertDietPlan(DietPlanModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_InsertDietPlan", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@goal", obj.Goal_Type);
            cmd.Parameters.AddWithValue("@day", obj.Day_Number);
            cmd.Parameters.AddWithValue("@meal", obj.Meal_Type);
            cmd.Parameters.AddWithValue("@food", obj.Food_Items);
            cmd.Parameters.AddWithValue("@cal", obj.Calories);

            if (con.State == ConnectionState.Closed)
                con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            return i;
        }
        public List<DietPlanModel> GetDietPlansByGoal(string goal)
        {
            List<DietPlanModel> list = new List<DietPlanModel>();

            SqlCommand cmd = new SqlCommand("sp_GetDietPlansByGoal", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@goal", goal);

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                DietPlanModel obj = new DietPlanModel();

                obj.Diet_Id = Convert.ToInt32(dr["Diet_Id"]);
                obj.Goal_Type = dr["Goal_Type"].ToString();
                obj.Day_Number = Convert.ToInt32(dr["Day_Number"]);
                obj.Meal_Type = dr["Meal_Type"].ToString();
                obj.Food_Items = dr["Food_Items"].ToString();
                obj.Calories = Convert.ToInt32(dr["Calories"]);

                list.Add(obj);
            }

            con.Close();
            return list;
        }
        public int AssignPlanToUser(AssignPlanModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_AssignPlanToUser", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userId", obj.User_Id);
            cmd.Parameters.AddWithValue("@planId", obj.Plan_Id);
            cmd.Parameters.AddWithValue("@dietId", obj.Diet_Id);

            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();

            return result;
        }
        public DataTable GetUsers()
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_GetAllUserIds", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetWorkoutPlans_DT()
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_GetWorkoutPlanList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable GetDietPlans()
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_GetDietPlanList", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int GetTotalUsers()
        {
            SqlCommand cmd = new SqlCommand("sp_TotalUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return count;
        }

        public int GetTotalWorkoutPlans()
        {
            SqlCommand cmd = new SqlCommand("sp_TotalWorkoutPlans", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return count;
        }

        public int GetTotalDietPlans()
        {
            SqlCommand cmd = new SqlCommand("sp_TotalDietPlans", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();

            return count;
        }

    }
}
