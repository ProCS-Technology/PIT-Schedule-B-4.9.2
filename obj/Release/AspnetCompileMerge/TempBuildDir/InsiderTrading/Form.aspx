<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="Form.aspx.cs" Inherits="ProcsDLL.InsiderTrading.Form" %>

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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="page-content-inner">


        <div class="col-md-12">
            <div class="portlet light portlet-fit" style="margin-left: -15px; margin-right: -15px; min-height: 1800px; min-width:1325px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Forms</span>
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <label>Form Selection</label>
                            <select id="selFormSelection">
                                <option value="0"></option>
                                <option value="Form_A">Form A</option>
                                <option value="Form_B">Form B</option>
                                <option value="Form_C">Form C</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label>Quarter Selection</label>
                            <select id="">
                                <option value="0"></option>
                                <option value="Quarter_1">Quarter 1</option>
                                <option value="Quarter_2">Quarter 2</option>
                                <option value="Quarter_3">Quarter 3</option>
                                <option value="Quarter_4">Quarter 4</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <label>Year Selection</label>
                            <select id="">
                                <option value="0"></option>
                                <option value="Year_2018">2018</option>
                                <option value="Year_2019">2019</option>
                                <option value="Year_2020">2020</option>
                                <option value="Year_2021">2021</option>
                                <option value="Year_2022">2022</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <button onclick="javascript:fnGoToForm()">Go</button>
                            <button style="display: none; margin-left: 20px;" id="btDownloadFormToPdf" onclick="javascript:fnDownloadFormToPdf()">Pdf</button><br />
                        </div>
                    </div>

                    <div id="divFormA" class="col-md-12" style="display: none">
                        <hr />
                        <div style="text-align: center"><b><u>FORM A</u></b></div>
                        <br />
                        <br />
                        <div style="text-align: center"><b>SEBI (Prohibition of Insider Trading) Regulations, 2015</b></div>
                        <br />
                        <div style="text-align: center"><b>[Regulation 7 (1) (a) read with Regulation 6 (2) – Initial disclosure to the company]</b></div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <span>Name of the company: <u>Minda Corporation Limited</u></span><br />
                        <br />
                        <span>ISIN of the company: <u>AT0000697750</u></span><br />
                        <br />
                        <span><b>Details of Securities held by Promoter, Key Managerial Personnel (KMP), Director and other such persons as mentioned in Regulation 6(2)</b></span>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setup1" style="width:85%">
                            <thead>
                                <tr>
                                    <th rowspan="2">Name, PAN, CIN/DIN & address with contact nos.</th>
                                    <th rowspan="2">Category of Person (Promoters/ KMP / Directors/immediate relative to/others etc)</th>
                                    <th style="text-align: center" colspan="2">Securities held as on the date of regulation coming into force </th>
                                    <th rowspan="2">% of Shareholding</th>
                                </tr>
                                <tr>
                                    <th>Type of security (For eg. – Shares, Warrants, Convertible Debentures etc.)</th>
                                    <th>No.  </th>
                                </tr>
                            </thead>
                            <tbody id="tbdForm1">
                                <tr>
                                    <td>
                                        <span>Akshay</span>
                                        <br />
                                        <span>AVNPA3600F</span>
                                        <br />
                                        <span>T-1516</span>
                                        <br />
                                        <span>sa park</span>
                                        <br />
                                        <span>1234567890</span>
                                    </td>
                                    <td>
                                        <span>Director</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>200000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>20</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Sanjay</span>
                                        <br />
                                        <span>AVNPA3600G</span>
                                        <br />
                                        <span>T-1517</span>
                                        <br />
                                        <span>sa park 2</span>
                                        <br />
                                        <span>1234567891</span>
                                    </td>
                                    <td>
                                        <span>Employee</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>150000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>15</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Abhishek</span>
                                        <br />
                                        <span>AVNPA3600H</span>
                                        <br />
                                        <span>T-1517</span>
                                        <br />
                                        <span>sa park 3</span>
                                        <br />
                                        <span>1234567892</span>
                                    </td>
                                    <td>
                                        <span>KMP</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>125000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>12.5</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Arvind</span>
                                        <br />
                                        <span>AVNPA3600I</span>
                                        <br />
                                        <span>T-1518</span>
                                        <br />
                                        <span>sa park 4</span>
                                        <br />
                                        <span>1234567893</span>
                                    </td>
                                    <td>
                                        <span>Promoter</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>75000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>7.5</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div><b>Note: </b>“Securities” shall have the meaning as defined under regulation 2(1)(i) of SEBI (Prohibition of Insider Trading) Regulations, 2015. </div>
                        <br />
                        <br />
                        <div  style="width:85%"><b>Details  of  Open  Interest  (OI)  in  derivatives  of  the  company  held  by  Promoter,  Key  Managerial  Personnel  (KMP),  Director  and  other  such  persons  as  mentioned  in  Regulation  6(2)</b></div>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setup2" style="width:85%">
                            <thead>
                                <tr>
                                    <th colspan="3">Open Interest of  the Future contracts held as on the date of  regulation coming into force</th>
                                    <th colspan="3">Open Interest of the Option Contracts held as on the date of  regulation coming into force</th>
                                </tr>
                                <tr>
                                    <th>Contract Specifications</th>
                                    <th>Number of units (contracts * lot size)</th>
                                    <th>Notional value in Rupee terms</th>
                                    <th>Contract Specifications</th>
                                    <th>Number of units (contracts * lot size)</th>
                                    <th>Notional value in Rupee terms</th>
                                </tr>
                            </thead>
                            <tbody id="tbdForm2">
                                <tr>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                </tr>
                            </tbody>
                        </table>
                        <div><b>Note: </b><i>In case of Options, notional value shall be calculated based on premium plus strike price of options</i></div>
                        <br />
                        <br />
                        <span>Name & Signature:</span><br />
                        <span>Designation:</span><br />
                        <span>Date:</span><br />
                        <span>Place:</span>
                    </div>

                    <div id="divFormB" class="col-md-12" style="display: none">
                        <hr />
                        <div style="text-align: center"><b><u>FORM B</u></b></div>
                        <br />
                        <br />
                        <div style="text-align: center"><b>SEBI (Prohibition of Insider Trading) Regulations, 2015</b></div>
                        <br />
                        <div style="text-align: center"><b>[Regulation 7 (1) (b) read with Regulation 6(2) – Disclosure on becoming a director/KMP/Promoter]</b></div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <span>Name of the company: <u>Minda Corporation Limited</u></span><br />
                        <br />
                        <span>ISIN of the company: <u>AT0000697750</u></span><br />
                        <br />
                        <span  style="width:85%"><b>Details of Securities held by Promoter, Key Managerial Personnel (KMP), Director and other such persons as mentioned in Regulation 6(2)</b></span>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setupB1" style="width:85%">
                            <thead>
                                <tr>
                                    <th rowspan="2">Name, PAN, CIN/DIN & address with contact nos.</th>
                                    <th rowspan="2">Category of Person (Promoters/ KMP / Directors/immediate relative to/others etc)</th>
                                    <th rowspan="2">Date of appointment of Director /KMP OR Date of becoming Promoter</th>
                                    <th style="text-align: center" colspan="2">Securities held as on the date of regulation coming into force </th>
                                    <th rowspan="2">% of Shareholding</th>
                                </tr>
                                <tr>
                                    <th>Type of security (For eg. – Shares, Warrants, Convertible Debentures etc.)</th>
                                    <th>No.  </th>
                                </tr>
                            </thead>
                            <tbody id="tbdFormB1">
                                <tr>
                                    <td>
                                        <span>Akshay</span>
                                        <br />
                                        <span>AVNPA3600F</span>
                                        <br />
                                        <span>T-1516</span>
                                        <br />
                                        <span>sa park</span>
                                        <br />
                                        <span>1234567890</span>
                                    </td>
                                    <td>
                                        <span>Director</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>200000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>20</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Sanjay</span>
                                        <br />
                                        <span>AVNPA3600G</span>
                                        <br />
                                        <span>T-1517</span>
                                        <br />
                                        <span>sa park 2</span>
                                        <br />
                                        <span>1234567891</span>
                                    </td>
                                    <td>
                                        <span>Employee</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>150000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>15</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Abhishek</span>
                                        <br />
                                        <span>AVNPA3600H</span>
                                        <br />
                                        <span>T-1517</span>
                                        <br />
                                        <span>sa park 3</span>
                                        <br />
                                        <span>1234567892</span>
                                    </td>
                                    <td>
                                        <span>KMP</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>125000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>12.5</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>Arvind</span>
                                        <br />
                                        <span>AVNPA3600I</span>
                                        <br />
                                        <span>T-1518</span>
                                        <br />
                                        <span>sa park 4</span>
                                        <br />
                                        <span>1234567893</span>
                                    </td>
                                    <td>
                                        <span>Promoter</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>75000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>7.5</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div><b>Note: </b>“Securities” shall have the meaning as defined under regulation 2(1)(i) of SEBI (Prohibition of Insider Trading) Regulations, 2015. </div>
                        <br />
                        <br />
                        <div  style="width:85%"><b>Details  of  Open  Interest  (OI)  in  derivatives  of  the  company  held  by  Promoter,  Key  Managerial  Personnel  (KMP),  Director  and  other  such  persons  as  mentioned  in  Regulation  6(2)</b></div>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setup2" style="width:85%">
                            <thead>
                                <tr>
                                    <th colspan="3">Open Interest of  the Future contracts held as on the date of  regulation coming into force</th>
                                    <th colspan="3">Open Interest of the Option Contracts held as on the date of  regulation coming into force</th>
                                </tr>
                                <tr>
                                    <th>Contract Specifications</th>
                                    <th>Number of units (contracts * lot size)</th>
                                    <th>Notional value in Rupee terms</th>
                                    <th>Contract Specifications</th>
                                    <th>Number of units (contracts * lot size)</th>
                                    <th>Notional value in Rupee terms</th>
                                </tr>
                            </thead>
                            <tbody id="tbdFormB2">
                                <tr>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                </tr>
                            </tbody>
                        </table>
                        <div><b>Note: </b><i>In case of Options, notional value shall be calculated based on premium plus strike price of options</i></div>
                        <br />
                        <br />
                        <span>Name & Signature:</span><br />
                        <span>Designation:</span><br />
                        <span>Date:</span><br />
                        <span>Place:</span>
                    </div>

                     <div id="divFormC" class="col-md-12" style="display: none">
                        <hr />
                        <div style="text-align: center"><b><u>FORM C</u></b></div>
                        <br />
                        <br />
                        <div style="text-align: center"><b>SEBI (Prohibition of Insider Trading) Regulations, 2015</b></div>
                        <br />
                        <div style="text-align: center"><b>[Regulation 7 (1) (b) read with Regulation 6(2) – Disclosure on becoming a director/KMP/Promoter]</b></div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <span>Name of the company: <u>Minda Corporation Limited</u></span><br />
                        <br />
                        <span>ISIN of the company: <u>AT0000697750</u></span><br />
                        <br />
                        <span ><b>Details of Securities held by Promoter, Key Managerial Personnel (KMP), Director and other such persons as mentioned in Regulation 6(2)</b></span>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setupC1">
                            <thead>
                                <tr>
                                    <th rowspan="2">Name, PAN, CIN/DIN & address with contact nos.</th>
                                    <th  rowspan="2">Category of Person (Promoters/ KMP / Directors/immediate relative to/others etc)</th>                        
                                    <th style="text-align: center" colspan="2">Securities held prior to acquisition/disposal </th>
                                    <th style="text-align: center" colspan="4">Securities acquired/Dispose </th>   
                                    <th style="text-align:center" colspan="2">Securities held post acquisition/disposal </th>   
                                    <th style="text-align:center" colspan="2">Date of allotment advice/ acquisition of shares/ sale of shares specify </th> 
                                    <th rowspan="2">Date of intimation to company </th> 
                                    <th rowspan="2">Mode of acquisition / disposal (on market/public/ rights/ preferential offer / off market/  Inter-se transfer, ESOPs etc.)  </th>                                
                                </tr>
                                <tr>
                                    <th>Type of security (For eg. – Shares, Warrants, Convertible Debentures etc.) </th>
                                    <th>No. and % of shareholding  </th>                                 
                                    <th >Type of security (For eg. – Shares, Warrants, Convertible Debentures etc.)</th>
                                    <th >No.</th>
                                    <th>Value </th>
                                    <th>Transaction Type (Buy/ Sale/ Pledge / Revoke/Invoke) </th>
                                    <th>Type of security (For eg. – Shares, Warrants, Convertible Debentures etc.) </th>
                                    <th>No. and % of shareholding </th>  
                                    <th >From</th>
                                    <th>To</th>                         
                                </tr>       
                                                    
                            </thead>
                            <tbody id="tbdFormC1">
                                <tr>
                                    <td>
                                        <span>Akshay</span>
                                        <br />
                                        <span>AVNPA3600F</span>
                                        <br />
                                        <span>T-1516</span>
                                        <br />
                                        <span>sa park</span>
                                        <br />
                                        <span>1234567890</span>
                                    </td>
                                    <td>
                                        <span>Director</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>200000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>20</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>201</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>202</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>203</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>204</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>205</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>206</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>207</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>208</span>
                                    </td>
                                </tr>      
                                 <tr>
                                    <td>
                                        <span>Abhinav</span>
                                        <br />
                                        <span>AVNPA3600q</span>
                                        <br />
                                        <span>T-1516</span>
                                        <br />
                                        <span>sa park</span>
                                        <br />
                                        <span>646534535</span>
                                    </td>
                                    <td>
                                        <span>ceo</span>
                                    </td>
                                    <td>
                                        <span>10/01/2015</span>
                                    </td>
                                    <td>
                                        <span>Equity</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>200000</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>20</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>201</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>202</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>203</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>204</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>205</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>206</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>207</span>
                                    </td>
                                    <td style="text-align: center">
                                        <span>208</span>
                                    </td>
                                </tr>                     
                            </tbody>
                        </table>
                        <div><b>Note: </b>“Securities” shall have the meaning as defined under regulation 2(1)(i) of SEBI (Prohibition of Insider Trading) Regulations, 2015. </div>
                        <br />
                        <br />
                        <div><b>Details  of  Open  Interest  (OI)  in  derivatives  of  the  company  held  by  Promoter,  Key  Managerial  Personnel  (KMP),  Director  and  other  such  persons  as  mentioned  in  Regulation  6(2)</b></div>
                        <br />
                        <br />
                        <table class="table table-striped table-hover table-bordered" id="tbl-Form-setup2" style="width:100%">
                            <thead>
                                <tr>
                                    <th colspan="6">Open Interest of  the Future contracts held as on the date of  regulation coming into force</th>
                                    <th colspan="6" rowspan="3" style="text-align:center">Exchange on which the trade was executed</th>
                                </tr>
                                <tr>
                                    <th rowspan="2" style="text-align:center">Type of contract</th>
                                    <th rowspan="2" style="text-align:center">Contract specifications</th>
                                    <th colspan="2" style="text-align:center">Buy</th>
                                    <th colspan="2" style="text-align:center">Sell</th>                                                                                          
                                </tr>     
                                 <tr>
                                    <th>Notional Value </th>
                                    <th >Number of units (contracts * lot size)</th>
                                    <th >Notional Value</th>
                                    <th >Number of units (contracts * lot size)</th>                                                                                          
                                </tr>                                                                                                                 
                            </thead>
                            <tbody id="tbdFormC2">
                                <tr>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                </tr>
                            </tbody>
                        </table>
                        <div><b>Note: </b><i>In case of Options, notional value shall be calculated based on premium plus strike price of options</i></div>
                        <br />
                        <br />
                        <span>Name & Signature:</span><br />
                        <span>Designation:</span><br />
                        <span>Date:</span><br />
                        <span>Place:</span>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <script src="../assets/global/scripts/datatable.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/datatables.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/Form.js" type="text/javascript"></script>
</asp:Content>
