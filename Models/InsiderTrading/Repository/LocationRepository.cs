using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;

namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class LocationRepository : IRequiresSessionState
    {
        public LocationResponse GetLocationList(Location objLocation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[0] = new SqlParameter("@Mode", "GET_LOCATION_LIST");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@COMPANY_ID", objLocation.companyId);

                LocationResponse oLocation = new LocationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOCATION", objLocation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Location obj = new Location();
                        obj.locationId = Convert.ToInt32(rdr["LOCATION_ID"]);
                        obj.locationName = !String.IsNullOrEmpty(rdr["LOCATION_NAME"].ToString()) ? Convert.ToString(rdr["LOCATION_NAME"]) : String.Empty;
                        obj.created_by = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        string dr = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dr;


                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        oLocation.AddObject(obj);
                    }
                    oLocation.StatusFl = true;
                    oLocation.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "No data found !";
                }
                rdr.Close();
                return oLocation;
            }
            catch (Exception ex)
            {
                LocationResponse oLocation = new LocationResponse();
                oLocation.StatusFl = false;
                oLocation.Msg = "Processing failed, because of system error !";
                return oLocation;
            }
        }

        public LocationResponse SaveLocation(Location objLocation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "INSERT_LOCATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOCATION_ID", objLocation.locationId);
                parameters[3] = new SqlParameter("@LOCATION_NAME", objLocation.locationName);
                parameters[4] = new SqlParameter("@CREATED_BY", objLocation.created_by);
                parameters[5] = new SqlParameter("@COMPANY_ID", objLocation.companyId);

                LocationResponse oLocation = new LocationResponse();
                int rc = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOCATION", objLocation.MODULE_DATABASE, parameters);

                var count = parameters[1].Value;
                Int32 set_count = Convert.ToInt32(count);
                if (set_count == 0)
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "Location already Exists !";
                    return oLocation;
                }

                if (rc > 0)
                {

                    oLocation.StatusFl = true;
                    oLocation.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "No data found !";
                }

                return oLocation;
            }
            catch (Exception ex)
            {
                LocationResponse oLocation = new LocationResponse();
                oLocation.StatusFl = false;
                oLocation.Msg = "Processing failed, because of system error !";
                return oLocation;
            }
        }

        public LocationResponse UpdateLocation(Location objLocation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[0] = new SqlParameter("@Mode", "UPDATE_LOCATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOCATION_ID", objLocation.locationId);
                parameters[3] = new SqlParameter("@LOCATION_NAME", objLocation.locationName);
                parameters[4] = new SqlParameter("@CREATED_BY", objLocation.created_by);
                parameters[5] = new SqlParameter("@COMPANY_ID", objLocation.companyId);

                LocationResponse oLocation = new LocationResponse();
                int rc = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOCATION", objLocation.MODULE_DATABASE, parameters);


                if (rc > 0)
                {

                    oLocation.StatusFl = true;
                    oLocation.Msg = "Data has been Saved successfully !";
                }
                else
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "No data found !";
                }

                return oLocation;
            }
            catch (Exception ex)
            {
                LocationResponse oLocation = new LocationResponse();
                oLocation.StatusFl = false;
                oLocation.Msg = "Processing failed, because of system error !";
                return oLocation;
            }
        }
        public LocationResponse EditLocation(Location objLocation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "EDIT_LOCATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOCATION_ID", objLocation.locationId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objLocation.companyId);

                LocationResponse oLocation = new LocationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOCATION", objLocation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Location obj = new Location();
                        obj.locationId = Convert.ToInt32(rdr["LOCATION_ID"]);
                        obj.locationName = !String.IsNullOrEmpty(rdr["LOCATION_NAME"].ToString()) ? Convert.ToString(rdr["LOCATION_NAME"]) : String.Empty;
                        obj.created_by = !String.IsNullOrEmpty(rdr["CREATED_BY"].ToString()) ? Convert.ToString(rdr["CREATED_BY"]) : String.Empty;
                        DateTime dt = Convert.ToDateTime(rdr["CREATED_ON"]);
                        string dr = dt.ToString("dd/MM/yyyy");
                        obj.created_on = dr;


                        obj.companyId = Convert.ToInt32(rdr["COMPANY_ID"]);
                        oLocation.AddObject(obj);
                    }
                    oLocation.StatusFl = true;
                    oLocation.Msg = "Data has been fetched successfully !";
                }
                else
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "No data found !";
                }
                rdr.Close();
                return oLocation;
            }
            catch (Exception ex)
            {
                LocationResponse oLocation = new LocationResponse();
                oLocation.StatusFl = false;
                oLocation.Msg = "Processing failed, because of system error !";
                return oLocation;
            }
        }

        public LocationResponse DeleteLocation(Location objLocation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@Mode", "DELETE_LOCATION");
                parameters[1] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2] = new SqlParameter("@LOCATION_ID", objLocation.locationId);
                parameters[3] = new SqlParameter("@COMPANY_ID", objLocation.companyId);

                LocationResponse oLocation = new LocationResponse();
                int status = SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_LOCATION", objLocation.MODULE_DATABASE, parameters);
                var st = parameters[1].Value;
                Int32 set_st = Convert.ToInt32(st);
                if (set_st == 0)
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "Unable to Delete. This Location is Used in Higher Component!";
                    return oLocation;
                }
                if (set_st > 0)
                {

                    oLocation.StatusFl = true;
                    oLocation.Msg = "Data has been Deleted successfully !";
                }
                else
                {
                    oLocation.StatusFl = false;
                    oLocation.Msg = "No data found !";
                }

                return oLocation;
            }
            catch (Exception ex)
            {
                LocationResponse oLocation = new LocationResponse();
                oLocation.StatusFl = false;
                oLocation.Msg = "Processing failed, because of system error !";
                return oLocation;
            }
        }
    }
}