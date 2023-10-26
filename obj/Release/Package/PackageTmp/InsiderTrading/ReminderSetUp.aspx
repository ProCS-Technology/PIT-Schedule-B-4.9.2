<%@ Page Title="" Language="C#" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" AutoEventWireup="true" CodeBehind="ReminderSetUp.aspx.cs" Inherits="ProcsDLL.InsiderTrading.ReminderSetUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/css/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    
   
    
    
    <%-- <link href="../assets/global/plugins/bootstrap-summernote/summernote.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .ck.ck-reset, .ck.ck-reset_all, .ck.ck-reset_all * {
            margin: 0;
            padding: 0;
            border: 0;
            background: transparent;
            text-decoration: none;
            vertical-align: middle;
            transition: none;
            word-wrap: break-word;
            z-index: 10200 !important;
        }
        .ck-mentions .mention__item {
              display: flex;
              align-items: center;
              z-index: 10200 !important;
            }
            .ck-mentions .mention__item img {
              border-radius: 100%;
              height: 30px;
              z-index: 10200 !important;
            }
            .ck-mentions .mention__item span {
              margin-left: 0.5em;
              z-index: 10200 !important;
            }
            .ck-mentions .mention__item.ck-on span {
              color: var(--ck-color-base-background);
              z-index: 10200 !important;
            }
            .ck-mentions .mention__item .mention__item__full-name {
              color: hsl(0deg, 0%, 45%);
              z-index: 10200 !important;
            }
            .ck-mentions .mention__item:hover:not(.ck-on) .mention__item__full-name {
              color: hsl(0deg, 0%, 40%);
              z-index: 10200 !important;
            }
            .ck.ck-color-ui-dropdown {
                  --ck-color-grid-tile-size: 20px;
                }
                .ck.ck-color-ui-dropdown .ck-color-grid {
                  grid-gap: 1px;
                }
                .ck.ck-color-ui-dropdown .ck-color-grid .ck-button {
                  border-radius: 0;
                }
                .ck.ck-color-ui-dropdown .ck-color-grid__tile:hover:not(.ck-disabled),
                .ck.ck-color-ui-dropdown .ck-color-grid__tile:focus:not(.ck-disabled) {
                  z-index: -1;
                  transform: scale(1.3);
                }
        :root {
            --ck-mention-list-max-height: 20px;
        }
         .ck {
                height: 200px;
         }
        .requied {
            color: red;
        }

        .required-red {
            /*color: red !important;*/
            
            border-color: red !important;
        }
         .required-red-border {
            color: red !important;
            border: 1px solid red;
            border-color: red !important;
        }
         .select2-container--default.select2-container--focus .select2-selection--multiple {
              border: 1px solid red!important;
            }

select {
  width: 100%;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      
<div class="portlet light bordered">
<div class="portlet-title">
            <div class="caption font-red-sunglo">
                <i class="icon-settings font-red-sunglo"></i>
                <span class="caption-subject bold uppercase">Reminder Set Up</span>
            </div>
        </div>

        <div class="portlet-body">           
            <div class="table-toolbar">
                <div class="row">
                    <div class="col-md-6">
                        <div class="btn-group-devided">
                            <button id="btnAddReminder"  class="btn green">
                                Add New Reminder
                            </button>
                            &nbsp;
                        </div>
                    </div>
                </div>
            </div>
            <table class="table table-striped table-hover table-bordered" id="tbl-reminder-setup">
                <thead>
                    <tr>
                        <%--<th>SNo</th>--%>
                        <th>Reminder Name</th>
                       <th>Type</th>
                        <th>Frequency</th>
                        <%--<th>Status</th>--%>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody id="tbdreminderlist">
                </tbody>
            </table>
        </div>

    


     <div class="modal fade" id="stack1" tabindex="-1" data-backdrop="static" data-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Reminder</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body py-10">
               <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <form class="form-horizontal" runat="server" role="form">
                                    <div class="form-body modal-fixheight" id="">
                                    
                                        <div class="form-group">
                                        <label class="col-md-3 control-label" id="lblreminderName"><b>Reminder</b></label>
                                        <div class="col-md-6">
                                            <select id="reminderName" class="form-control form-control-inline" onchange="removRequried(this)">

                                            </select>
                                             <%--<input type="text" class="form-control form-control-inline" id="reminderName"  onchange="removRequried(this)" />--%>
                                                  <input type="text" class="from-control" id="reminderID"  style="display:none"/>
                                        </div>
                                    </div><br />
                                        <div class="form-group">
                                        <label class="col-md-3 control-label" id="lbltypeofReminder"><b>Reminder Type</b></label>
                                        <div class="col-md-6">
                                              <select id="typeofReminder" class="form-control form-control-inline" onchange="removRequried(this)">
                                                    <option value="0">Select</option>
                                                    <option value="One Time">One Time</option>
                                                    <option value="Days">Days</option>
                                                    <option value="Hours">Hours</option>
                                                </select>
                                        </div>
                                    </div><br />
                                         <div class="form-group">
                                        <label class="col-md-3 control-label" id="lbltypeValue"><b>Reminder Value</b></label>
                                        <div class="col-md-6">
                                              <input type="number" min="0" class="form-control form-control-inline" id="typeValue" onchange="removRequried(this)"/>
                                        </div>
                                    </div><br />
                                         <div class="form-group">
                                        <label class="col-md-3 control-label" id="lblDuration"><b>Duration</b></label>
                                        <div class="col-md-6">
                                              <input type="number" min="0" class="form-control form-control-inline" id="txtDuration" onchange="removRequried(this)"/>
                                        </div>
                                    </div><br />
                                        <div class="form-group">
                                        <label class="col-md-3 control-label" id="lblSubject"><b>Mail Subject</b></label>
                                        <div class="col-md-6">
                                              <input type="text" class="form-control form-control-inline" id="txtSubject" onchange="removRequried(this)"/>
                                        </div>
                                    </div><br />
                                         <div class="form-group">
                                        <label class="col-md-3 control-label" id="lbltextArea"><b>Mail Template</b></label>
                                        <div class="col-md-9">
                                            <div id="divTextarea">
                                                <textarea id="txtTemplate" name="txtTemplate" class="form-control form-control-solid"></textarea>
                                            </div>
                                            <%--<div id="txtarea1" style="display:none">
                                                <textarea id="txtTemplate1" name="txtTemplate" class="form-control form-control-solid" style="display:none"></textarea>
                                            </div>--%>
                                        </div>
                                    </div><br />
                                          <div class="form-group display-none">
                                               <label class="col-md-3 control-label" id="lblField"><b>Field Name</b></label>
                                        <div class="col-md-6"> 
                                            <%--<asp:DropDownList ID="ddlField" runat="server" CssClass="form-control form-control-inline" OnSelectedIndexChanged="ddlField_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="[User Name]" Text="User Name"></asp:ListItem>
                                                <asp:ListItem Value="[Designation]" Text="Designation"></asp:ListItem>
                                                <asp:ListItem Value="[Department]" Text="Department">Department</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <select id="ddlField" class="form-control form-control-inline">                                                    
                                                </select>
                                        </div></div><br />                                       
                             </div>
   

                                   
                             </form>
                         </div>
                    <div class="form-actions">
                                <div class="row" style="text-align: center">
                                    <div class="col-md-offset-4 col-md-12">
                                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSaveupsi();">Save</button>
                                        <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                                    </div>
                                </div>
                            </div>

                   </div>
                </div>
            </div>
        </div>
    </div>



   







   
     <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
        <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
        <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/scripts/jquery-ui.min.js" type="text/javascript"></script>

        <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
        <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
           
    <script src="../assets/plugins/custom/ckeditor/ckeditor-mention.js" type="text/javascript"></script>
   <script src="https://cdn.ckeditor.com/4.9.2/standard/ckeditor.js"></script>
    <%--  <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>    --%>
   <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>    
    <script src="js/ReminderSetUp.js?<%=DateTime.Now %>" type="text/javascript"></script>
      <%--  <script type="text/javascript">
            //$(document).ready(function () {
            //    $('.summernote').summernote({
            //        toolbar: [
            //            ['style', ['style']],
            //            ['font', ['bold', 'italic', 'underline', 'clear']],
            //            ['fontname', ['fontname']],
            //            ['color', ['color']],
            //            ['para', ['ul', 'ol', 'paragraph']],
            //            ['height', ['height']],
            //            ['table', ['table']],
            //            ['insert', ['link', 'hr']],
            //            ['view', ['fullscreen', 'codeview']],
            //            ['help', ['help']]
            //        ]
            //    });               

            //});
            $("#Loader").hide();
        </script>--%>
    </div>
    
</asp:Content>                              
