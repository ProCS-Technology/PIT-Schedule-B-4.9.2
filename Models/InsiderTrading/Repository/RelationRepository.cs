using ProcsDLL.Models.Infrastructure;
using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
namespace ProcsDLL.Models.InsiderTrading.Repository
{
    public class RelationRepository : IRequiresSessionState
    {
        public RelationResponse AddRelation(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[10];
                parameters[0] = new SqlParameter("@RELATION_ID", objRelation.RELATION_ID);
                parameters[1] = new SqlParameter("@RELATION_NM", objRelation.RELATION_NM);
                parameters[2] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[3] = new SqlParameter("@Mode", "CHECK");
                parameters[4] = new SqlParameter("@ACTION_TYPE", "INSERT");
                parameters[5] = new SqlParameter("@EMPLOYEE_ID", objRelation.CREATED_BY);
                parameters[6] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[6].Direction = ParameterDirection.Output;
                parameters[7] = new SqlParameter("@IS_MANDATORY", objRelation.IS_MANDATORY);
                parameters[8] = new SqlParameter("@ORDER_SEQ", objRelation.ORDER_SEQUENCE);

                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);
                var obj = parameters[6].Value;
                RelationResponse oRel = new RelationResponse();
                if ((Int32)obj == 0)
                {
                    parameters[3] = new SqlParameter("@Mode", "INSERT_UPDATE");
                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);
                    parameters[3] = new SqlParameter("@Mode", "GET_RELATION_ID_BY_RELATION_NAME");
                    var RelationId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);
                    objRelation.RELATION_ID = (Int32)RelationId;

                    oRel.StatusFl = true;
                    oRel.Msg = "Data has been saved successfully !";
                    oRel.Relation = objRelation;
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "Relation Name Or Order Sequence already exists !";
                }
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RelationResponse UpdateRelation(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[9];
                parameters[0] = new SqlParameter("@RELATION_ID", objRelation.RELATION_ID);
                parameters[1] = new SqlParameter("@RELATION_NM", objRelation.RELATION_NM);
                parameters[2] = new SqlParameter("@Mode", "CHECK");
                parameters[3] = new SqlParameter("@ACTION_TYPE", "UPDATE");
                parameters[4] = new SqlParameter("@EMPLOYEE_ID", objRelation.CREATED_BY);
                parameters[5] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[5].Direction = ParameterDirection.Output;
                parameters[6] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[7] = new SqlParameter("@IS_MANDATORY", objRelation.IS_MANDATORY);
                parameters[8] = new SqlParameter("@ORDER_SEQ", objRelation.ORDER_SEQUENCE);


                SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);
                var obj = parameters[5].Value;
                RelationResponse oRel = new RelationResponse();
                if ((Int32)obj == 0)
                {
                    parameters[2] = new SqlParameter("@Mode", "INSERT_UPDATE");

                    SQLHelper.ExecuteNonQuery(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);

                    parameters[2] = new SqlParameter("@Mode", "GET_RELATION_ID_BY_RELATION_NAME");
                    var RelationId = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);

                    if ((Int32)RelationId == -1)
                    {
                        oRel.StatusFl = false;
                        oRel.Msg = "Relation Name Does Not Exist !";
                    }
                    else
                    {
                        objRelation.RELATION_ID = (Int32)RelationId;
                        oRel.StatusFl = true;
                        oRel.Msg = "Data has been updated successfully !";
                        oRel.Relation = objRelation;
                    }
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "Order Sequence already exists !";
                }
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RelationResponse DeleteRelation(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@RELATION_ID", objRelation.RELATION_ID);
                parameters[1] = new SqlParameter("@Mode", "DELETE_RELATION");
                parameters[2] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;
                RelationResponse oRel = new RelationResponse();

                var result = SQLHelper.ExecuteScalar(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);
                oRel.StatusFl = true;
                oRel.Msg = "Data has been deleted successfully !";
                oRel.Relation = objRelation;
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RelationResponse GetRelationList(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_RELATION_LIST");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRelation.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RelationResponse oRel = new RelationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Relation obj = new Relation();

                        obj.RELATION_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.RELATION_NM = Convert.ToString(rdr.GetValue(1));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(4));
                        obj.IS_MANDATORY = Convert.ToString(rdr.GetValue(5));
                        obj.ORDER_SEQUENCE = Convert.ToInt32(rdr.GetValue(6));

                        oRel.AddObject(obj);
                    }
                    oRel.StatusFl = true;
                    oRel.Msg = "Relation Data has been fetched successfully !";
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "No data found !";
                }
                rdr.Close();
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RelationResponse GetRelationForRelative(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_RELATION_LIST_FOR_RELATIVE");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRelation.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RelationResponse oRel = new RelationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Relation obj = new Relation();

                        obj.RELATION_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.RELATION_NM = Convert.ToString(rdr.GetValue(1));
                        obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(4));
                        obj.IS_MANDATORY = Convert.ToString(rdr.GetValue(2));
                        obj.ORDER_SEQUENCE = Convert.ToInt32(rdr.GetValue(3));

                        oRel.AddObject(obj);
                    }
                    oRel.StatusFl = true;
                    oRel.Msg = "Relation Data has been fetched successfully !";
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "No data found !";
                }
                rdr.Close();
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
        public RelationResponse GetRelationForDeclaration(Relation objRelation)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@COMPANY_ID", objRelation.COMPANY_ID);
                parameters[1] = new SqlParameter("@Mode", "GET_RELATION_LIST_FOR_DECLARATION");
                parameters[2] = new SqlParameter("@EMPLOYEE_ID", objRelation.CREATED_BY);
                parameters[3] = new SqlParameter("@SET_COUNT", SqlDbType.Int);
                parameters[3].Direction = ParameterDirection.Output;

                RelationResponse oRel = new RelationResponse();
                SqlDataReader rdr = SQLHelper.ExecuteReader(SQLHelper.GetConnString(), CommandType.StoredProcedure, "SP_PROCS_INSIDER_RELATION", objRelation.MODULE_DATABASE, parameters);

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Relation obj = new Relation();

                        obj.RELATION_ID = Convert.ToInt32(rdr.GetValue(0));
                        obj.RELATION_NM = Convert.ToString(rdr.GetValue(1));
                        //obj.COMPANY_ID = Convert.ToInt32(rdr.GetValue(4));
                        obj.IS_MANDATORY = Convert.ToString(rdr.GetValue(2));
                        obj.ORDER_SEQUENCE = Convert.ToInt32(rdr.GetValue(3));
                        oRel.AddObject(obj);
                    }
                    oRel.StatusFl = true;
                    oRel.Msg = "Relation Data has been fetched successfully !";
                }
                else
                {
                    oRel.StatusFl = false;
                    oRel.Msg = "No data found !";
                }
                rdr.Close();
                return oRel;
            }
            catch (Exception ex)
            {
                RelationResponse oRel = new RelationResponse();
                oRel.StatusFl = false;
                oRel.Msg = "Processing failed, because of system error !";
                return oRel;
            }
        }
    }
}