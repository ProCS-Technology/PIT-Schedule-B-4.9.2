<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Declaration_Bulk_Upload.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Declaration_Bulk_Upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--Start Datetime--%>
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <%--End Datetime--%>
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
            .requied{color:red;}
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- ========================= Department List ============================= --%>
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit ">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-settings font-red"></i>
                            <span class="caption-subject font-red sbold uppercase">Declaration Bulk Upload</span>
                        </div>
                    </div>
                    <div class="portlet-body">
                        <div class="table-toolbar">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="btn-group-devided">
                                       <button type="button" class="btn grey-mint" onclick="Download_Template()" id="Download">
                                                                    Download <i class="fa fa-download"></i>
                                         </button>&nbsp;&nbsp;
                                         <button type="button" class="btn grey-mint" onclick="Upload_Template()"  id="Upload">
                                                                    Upload <i class="fa fa-upload"></i>
                                          </button>&nbsp;&nbsp;
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="table table-striped table-hover table-bordered" id="tbl-Department-setup">
                            <thead>
                                <tr>  
                                    <th>Seq</th>                                  
                                    <th>Declaration File Name</th>    
                                    <th>Uploaded Date</th>
                                    <th>ACTION</th>
                                </tr>
                            </thead>
                            <tbody id="tbdDepartmentList">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- ========================= Add New Declaration Document ============================= --%>


     <div class="modal fade in" id="mdupload_file" tabindex="-1" role="dialog" aria-hidden="True">
            <div class="modal-dialog">
                <div class="modal-content">
                    <%--<div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    </div>--%>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <h4 class="modal-title">Please  select the file for upload</h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <br /><br /><br />
                                <input type="file" id="fileuploded" />
                            </div>
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                        
                        <button type="button" data-dismiss="modal" class="btn dark  btn-outline" onclick=" Save_Upload_Template();">Save</button>
                        <button id="close_file_upload" class="btn btn-info">Close</button>
                    </div>
                </div>
            </div>
        </div>
      <%-- ========================= Add New Record in Department List ============================= --%>
    <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">Department SetUp</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4" id="lblDepartment" style="margin-top: 5px;">Department Name </div>
                        <div class="col-md-6">
                            <input id="txtDepartmentName" class="form-control" type="text"/>
                            <input class="form-control" id="txtDepartmentKey" type="text" style="display: none;" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />
                    
                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Created By </div>
                        <div class="col-md-6">
                            <input id="txtCreateBy" class="form-control" type="text" data-tabindex="2" />
                            <input class="form-control" id="txtCreatedByKey" type="text" style="display: none;" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />--%>
                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Company Name </div>
                        <div class="col-md-6">
                            <select id="ddlCompany" class="form-control">
                                <option value="0"></option>
                                <option value="Active">Active
                                </option>
                                <option value="Inactive">Inactive
                                </option>
                            </select>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />--%>                  
                    <%-- <div class="row">
                        <div class="col-md-4">Subsciption End Date </div>
                        <div class="col-md-6">
                            <%--<input id="txtSED" class="form-control" type="text" data-tabindex="2" />-%>
                            <div class="input-group input-medium date date-picker" data-date-format="yyyy/mm/dd" data-date-start-date="+0d">
                                <input id="txtSED" class="form-control" readonly="readonly" type="text" />
                                <span class="input-group-btn">
                                    <button class="btn default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>--%>
                    <br />
                    <%--<div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Upload Logo </div>
                        <div class="col-md-6">
                            <input id="logo" class="form-control" type="file" data-tabindex="2" />
                        </div>
                        <div id="imagePreview"></div>
                        <div class="col-md-2"></div>
                    </div>--%>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseModal();">Close</button>
                    <button id="btnSave" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveDepartment();">Save</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <script src="js/Global.js"  type="text/javascript"></script>
   <script src="js/Declaration_Bulk_Upload.js"  type="text/javascript"></script>
   
</asp:Content>
