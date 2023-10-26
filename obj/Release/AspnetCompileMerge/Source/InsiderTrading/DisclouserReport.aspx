<%@ Page  Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="DisclouserReport.aspx.cs" Inherits="ProcsDLL.InsiderTrading.DisclouserReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/css/components.min.css" rel="stylesheet" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datetimepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/datatables/datatables.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style>
        .page-content {
            min-height: 800px !important;
        }

        table.dataTable tbody th,
        table.dataTable tbody td {
            white-space: nowrap;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12">
        <!-- BEGIN Portlet PORTLET-->
        <div class="portlet light portlet-fit ">
            <div class="portlet-title">
                <div class="caption">
                    <i class="icon-settings font-red"></i>
                    <span class="caption-subject font-red sbold uppercase">Disclouser Report</span>
                </div>
            </div>

            <div class="portlet-body">
                <div class="row">
                    <div class="col-md-3 col-lg-2">
                        <label for="Company" style="text-align: center; display: block;">Disclosure</label>
                        <select class="form-control select2" name="Company" id="bindDisclouser">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="User" style="text-align: center; display: block;">User</label>
                        <select class="form-control select2" name="User" id="bindUser">
                        </select>
                    </div>
                    <div class="col-md-3 col-lg-3">
                        <label for="statusname" style="text-align: center; display: block;">Status</label>
                        <select class="form-control select2" name="statusname" id="ddStatus">
                            <option value="0">All</option>
                            <option value="Open">Open</option>
                            <option value="Closed">Closed</option>
                        </select>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <button id="txtGetDisclouserReport" type="button" style="background-color: limegreen; margin-top: 25px;" class="form-control" onclick="fnGetDisclouserReport();" name="to_date">Run</button>
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <div class="row">
                    <table class="table table-striped table-hover display text-nowrap table-bordered" id="tblDisclouserReportsetup">
                        <thead>
                            <tr>
                                   <th><input type="checkbox" id="checkAll"/>  All</th>
                                   <th style="display:none">TaskId</th>
                                    <th> User</th>
                                    <th>Status</th>
                                    <th>Submitted On</th>
                                    <th>Form</th>
                            </tr>
                        </thead>
                        <tbody id="tbdDisclouserReportList">
                        </tbody>
                    </table>
                    <div class="col-md-2 col-lg-2">
                    <button type="button" id="sendmail"  class="btn btn-primary m-t-25" data-toggle="modal" data-target="#DeclarationMail" > Send Reminder</button>
                </div>
                </div>
            </div>
            <!-- END Portlet PORTLET-->
        </div>
    </div>
    <div class="modal fade" id="DeclarationMail" tabindex="-1" data-backdrop="static" data-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Mail Template</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                </div>
                <div class="modal-body py-10">
               <div class="portlet light bordered">
                            <div class="portlet-body form">
                                <form class="form-horizontal" runat="server" role="form">
                                    <div class="form-body modal-fixheight" id="">
                                       
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
                             </div>                  
                             </form>
                         </div>
                    <div class="form-actions">
                                <div class="row" style="text-align: center">
                                    <div class="col-md-offset-4 col-md-12">
                                        <button id="btnSave" type="button" class="btn green" onclick="javascript:fnSendMail();">Send Email</button>
                                        <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModal();">Cancel</button>
                                    </div>
                                </div>
                            </div>
                   </div>
                </div>
            </div>
        </div>
    </div>
    <%--Start Datetime--%>
    <script src="../assets/editor/jquery-te-1.4.0.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="../assets/global/plugins/bootstrap-timepicker/js/bootstrap-timepicker.min.js"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <%--End Datetime--%>

    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.min.js" type="text/javascript"></script>

    <script src="../assets/global/plugins/flot/jquery.flot.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.resize.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.categories.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.pie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.stack.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/flot/jquery.flot.crosshair.min.js" type="text/javascript"></script>
    <script src="../assets/plugins/custom/ckeditor/ckeditor-mention.js" type="text/javascript"></script>
    <script src="https://cdn.ckeditor.com/4.9.2/standard/ckeditor.js"></script>
    <%--  <script src="../assets/global/plugins/bootstrap-summernote/summernote.min.js" type="text/javascript"></script>    --%>
   <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>
    <script src="js/Global.js?<%=DateTime.Now %>" type="text/javascript"></script>
    <script src="js/DisclouserReport.js?<%=DateTime.Now %>" type="text/javascript"></script>
</asp:Content>
