<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="MyDetails.aspx.cs" Inherits="ProcsDLL.InsiderTrading.MyDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <style>
        .page-content {
            min-height: 1236px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="col-md-12">
            <div class="portlet light portlet-fit" style="margin-top: -25px; margin-left: -35px; margin-right: -35px; min-height: 1216px;">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="icon-settings font-red"></i>
                        <span class="caption-subject font-red sbold uppercase">Personal details</span>
                    </div>
                </div>
                <div class="portlet-body">

                    <div class="form-group" id="dvUsername">
                        <label class="control-label col-md-3">Username</label>
                        <div class="col-md-4">
                            <input id="txtUserName" disabled="disabled" type="text" runat="server" class="form-control" name="username" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvLoginId">
                        <label class="control-label col-md-3">Login ID</label>
                        <div class="col-md-4">
                            <input id="txtLoginId" disabled="disabled" runat="server" type="text" class="form-control" name="login_id" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvParentCompany">
                        <label class="control-label col-md-3">Company</label>
                        <div class="col-md-4">
                            <input id="txtParentCompanyName" disabled="disabled" runat="server" type="text" class="form-control" name="parent_company_name" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvCompany">
                        <label class="control-label col-md-3">Subsidiary</label>
                        <div class="col-md-4">
                            <input id="txtCompanyName" disabled="disabled" runat="server" type="text" class="form-control" name="company_name" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvEmail">
                        <label class="control-label col-md-3">Email</label>
                        <div class="col-md-4">
                            <input id="txtEmailId" disabled="disabled" runat="server" type="text" class="form-control" name="email" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvPersonalEmail">
                        <label class="control-label col-md-3">Personal Email</label>
                        <div class="col-md-4">
                            <input id="txtPersonalEmail" disabled="disabled" type="text" class="form-control" name="personal_email" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvRole">
                        <label class="control-label col-md-3">Role</label>
                        <div class="col-md-4">
                            <input id="txtRole" disabled="disabled" runat="server" type="text" class="form-control" name="role" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvEmployeeId">
                        <label class="control-label col-md-3">Employee No.</label>
                        <div class="col-md-4">
                            <input id="txtEmployeeId" disabled="disabled" runat="server" type="text" class="form-control" name="employee_id" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvResidentType">
                        <label class="control-label col-md-3">
                            Resident Type
                        </label>
                        <div class="col-md-4">
                            <select name="resident_type" disabled="disabled" runat="server" id="ddlResidentType" class="form-control select2">
                                <option value=""></option>
                                <option value="NRI">NRI</option>
                                <option value="FOREIGN_NATIONAL">FOREIGN NATIONAL</option>
                                <option value="INDIAN_RESIDENT">INDIAN RESIDENT</option>
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvCountry">
                        <label class="control-label col-md-3">
                            Country
                        </label>
                        <div class="col-md-4">
                            <select name="country" disabled="disabled" runat="server" id="country" class="form-control select2">
                                <option value=""></option>
                                <option value="AF">Afghanistan</option>
                                <option value="AL">Albania</option>
                                <option value="DZ">Algeria</option>
                                <option value="AS">American Samoa</option>
                                <option value="AD">Andorra</option>
                                <option value="AO">Angola</option>
                                <option value="AI">Anguilla</option>
                                <option value="AR">Argentina</option>
                                <option value="AM">Armenia</option>
                                <option value="AW">Aruba</option>
                                <option value="AU">Australia</option>
                                <option value="AT">Austria</option>
                                <option value="AZ">Azerbaijan</option>
                                <option value="BS">Bahamas</option>
                                <option value="BH">Bahrain</option>
                                <option value="BD">Bangladesh</option>
                                <option value="BB">Barbados</option>
                                <option value="BY">Belarus</option>
                                <option value="BE">Belgium</option>
                                <option value="BZ">Belize</option>
                                <option value="BJ">Benin</option>
                                <option value="BM">Bermuda</option>
                                <option value="BT">Bhutan</option>
                                <option value="BO">Bolivia</option>
                                <option value="BA">Bosnia and Herzegowina</option>
                                <option value="BW">Botswana</option>
                                <option value="BV">Bouvet Island</option>
                                <option value="BR">Brazil</option>
                                <option value="IO">British Indian Ocean Territory</option>
                                <option value="BN">Brunei Darussalam</option>
                                <option value="BG">Bulgaria</option>
                                <option value="BF">Burkina Faso</option>
                                <option value="BI">Burundi</option>
                                <option value="KH">Cambodia</option>
                                <option value="CM">Cameroon</option>
                                <option value="CA">Canada</option>
                                <option value="CV">Cape Verde</option>
                                <option value="KY">Cayman Islands</option>
                                <option value="CF">Central African Republic</option>
                                <option value="TD">Chad</option>
                                <option value="CL">Chile</option>
                                <option value="CN">China</option>
                                <option value="CX">Christmas Island</option>
                                <option value="CC">Cocos (Keeling) Islands</option>
                                <option value="CO">Colombia</option>
                                <option value="KM">Comoros</option>
                                <option value="CG">Congo</option>
                                <option value="CD">Congo, the Democratic Republic of the</option>
                                <option value="CK">Cook Islands</option>
                                <option value="CR">Costa Rica</option>
                                <option value="CI">Cote d'Ivoire</option>
                                <option value="HR">Croatia (Hrvatska)</option>
                                <option value="CU">Cuba</option>
                                <option value="CY">Cyprus</option>
                                <option value="CZ">Czech Republic</option>
                                <option value="DK">Denmark</option>
                                <option value="DJ">Djibouti</option>
                                <option value="DM">Dominica</option>
                                <option value="DO">Dominican Republic</option>
                                <option value="EC">Ecuador</option>
                                <option value="EG">Egypt</option>
                                <option value="SV">El Salvador</option>
                                <option value="GQ">Equatorial Guinea</option>
                                <option value="ER">Eritrea</option>
                                <option value="EE">Estonia</option>
                                <option value="ET">Ethiopia</option>
                                <option value="FK">Falkland Islands (Malvinas)</option>
                                <option value="FO">Faroe Islands</option>
                                <option value="FJ">Fiji</option>
                                <option value="FI">Finland</option>
                                <option value="FR">France</option>
                                <option value="GF">French Guiana</option>
                                <option value="PF">French Polynesia</option>
                                <option value="TF">French Southern Territories</option>
                                <option value="GA">Gabon</option>
                                <option value="GM">Gambia</option>
                                <option value="GE">Georgia</option>
                                <option value="DE">Germany</option>
                                <option value="GH">Ghana</option>
                                <option value="GI">Gibraltar</option>
                                <option value="GR">Greece</option>
                                <option value="GL">Greenland</option>
                                <option value="GD">Grenada</option>
                                <option value="GP">Guadeloupe</option>
                                <option value="GU">Guam</option>
                                <option value="GT">Guatemala</option>
                                <option value="GN">Guinea</option>
                                <option value="GW">Guinea-Bissau</option>
                                <option value="GY">Guyana</option>
                                <option value="HT">Haiti</option>
                                <option value="HM">Heard and Mc Donald Islands</option>
                                <option value="VA">Holy See (Vatican City State)</option>
                                <option value="HN">Honduras</option>
                                <option value="HK">Hong Kong</option>
                                <option value="HU">Hungary</option>
                                <option value="IS">Iceland</option>
                                <option value="IN">India</option>
                                <option value="ID">Indonesia</option>
                                <option value="IR">Iran (Islamic Republic of)</option>
                                <option value="IQ">Iraq</option>
                                <option value="IE">Ireland</option>
                                <option value="IL">Israel</option>
                                <option value="IT">Italy</option>
                                <option value="JM">Jamaica</option>
                                <option value="JP">Japan</option>
                                <option value="JO">Jordan</option>
                                <option value="KZ">Kazakhstan</option>
                                <option value="KE">Kenya</option>
                                <option value="KI">Kiribati</option>
                                <option value="KP">Korea, Democratic People's Republic of</option>
                                <option value="KR">Korea, Republic of</option>
                                <option value="KW">Kuwait</option>
                                <option value="KG">Kyrgyzstan</option>
                                <option value="LA">Lao People's Democratic Republic</option>
                                <option value="LV">Latvia</option>
                                <option value="LB">Lebanon</option>
                                <option value="LS">Lesotho</option>
                                <option value="LR">Liberia</option>
                                <option value="LY">Libyan Arab Jamahiriya</option>
                                <option value="LI">Liechtenstein</option>
                                <option value="LT">Lithuania</option>
                                <option value="LU">Luxembourg</option>
                                <option value="MO">Macau</option>
                                <option value="MK">Macedonia, The Former Yugoslav Republic of</option>
                                <option value="MG">Madagascar</option>
                                <option value="MW">Malawi</option>
                                <option value="MY">Malaysia</option>
                                <option value="MV">Maldives</option>
                                <option value="ML">Mali</option>
                                <option value="MT">Malta</option>
                                <option value="MH">Marshall Islands</option>
                                <option value="MQ">Martinique</option>
                                <option value="MR">Mauritania</option>
                                <option value="MU">Mauritius</option>
                                <option value="YT">Mayotte</option>
                                <option value="MX">Mexico</option>
                                <option value="FM">Micronesia, Federated States of</option>
                                <option value="MD">Moldova, Republic of</option>
                                <option value="MC">Monaco</option>
                                <option value="MN">Mongolia</option>
                                <option value="MS">Montserrat</option>
                                <option value="MA">Morocco</option>
                                <option value="MZ">Mozambique</option>
                                <option value="MM">Myanmar</option>
                                <option value="NA">Namibia</option>
                                <option value="NR">Nauru</option>
                                <option value="NP">Nepal</option>
                                <option value="NL">Netherlands</option>
                                <option value="AN">Netherlands Antilles</option>
                                <option value="NC">New Caledonia</option>
                                <option value="NZ">New Zealand</option>
                                <option value="NI">Nicaragua</option>
                                <option value="NE">Niger</option>
                                <option value="NG">Nigeria</option>
                                <option value="NU">Niue</option>
                                <option value="NF">Norfolk Island</option>
                                <option value="MP">Northern Mariana Islands</option>
                                <option value="NO">Norway</option>
                                <option value="OM">Oman</option>
                                <option value="PK">Pakistan</option>
                                <option value="PW">Palau</option>
                                <option value="PA">Panama</option>
                                <option value="PG">Papua New Guinea</option>
                                <option value="PY">Paraguay</option>
                                <option value="PE">Peru</option>
                                <option value="PH">Philippines</option>
                                <option value="PN">Pitcairn</option>
                                <option value="PL">Poland</option>
                                <option value="PT">Portugal</option>
                                <option value="PR">Puerto Rico</option>
                                <option value="QA">Qatar</option>
                                <option value="RE">Reunion</option>
                                <option value="RO">Romania</option>
                                <option value="RU">Russian Federation</option>
                                <option value="RW">Rwanda</option>
                                <option value="KN">Saint Kitts and Nevis</option>
                                <option value="LC">Saint LUCIA</option>
                                <option value="VC">Saint Vincent and the Grenadines</option>
                                <option value="WS">Samoa</option>
                                <option value="SM">San Marino</option>
                                <option value="ST">Sao Tome and Principe</option>
                                <option value="SA">Saudi Arabia</option>
                                <option value="SN">Senegal</option>
                                <option value="SC">Seychelles</option>
                                <option value="SL">Sierra Leone</option>
                                <option value="SG">Singapore</option>
                                <option value="SK">Slovakia (Slovak Republic)</option>
                                <option value="SI">Slovenia</option>
                                <option value="SB">Solomon Islands</option>
                                <option value="SO">Somalia</option>
                                <option value="ZA">South Africa</option>
                                <option value="GS">South Georgia and the South Sandwich Islands</option>
                                <option value="ES">Spain</option>
                                <option value="LK">Sri Lanka</option>
                                <option value="SH">St. Helena</option>
                                <option value="PM">St. Pierre and Miquelon</option>
                                <option value="SD">Sudan</option>
                                <option value="SR">Suriname</option>
                                <option value="SJ">Svalbard and Jan Mayen Islands</option>
                                <option value="SZ">Swaziland</option>
                                <option value="SE">Sweden</option>
                                <option value="CH">Switzerland</option>
                                <option value="SY">Syrian Arab Republic</option>
                                <option value="TW">Taiwan, Province of China</option>
                                <option value="TJ">Tajikistan</option>
                                <option value="TZ">Tanzania, United Republic of</option>
                                <option value="TH">Thailand</option>
                                <option value="TG">Togo</option>
                                <option value="TK">Tokelau</option>
                                <option value="TO">Tonga</option>
                                <option value="TT">Trinidad and Tobago</option>
                                <option value="TN">Tunisia</option>
                                <option value="TR">Turkey</option>
                                <option value="TM">Turkmenistan</option>
                                <option value="TC">Turks and Caicos Islands</option>
                                <option value="TV">Tuvalu</option>
                                <option value="UG">Uganda</option>
                                <option value="UA">Ukraine</option>
                                <option value="AE">United Arab Emirates</option>
                                <option value="GB">United Kingdom</option>
                                <option value="US">United States</option>
                                <option value="UM">United States Minor Outlying Islands</option>
                                <option value="UY">Uruguay</option>
                                <option value="UZ">Uzbekistan</option>
                                <option value="VU">Vanuatu</option>
                                <option value="VE">Venezuela</option>
                                <option value="VN">Viet Nam</option>
                                <option value="VG">Virgin Islands (British)</option>
                                <option value="VI">Virgin Islands (U.S.)</option>
                                <option value="WF">Wallis and Futuna Islands</option>
                                <option value="EH">Western Sahara</option>
                                <option value="YE">Yemen</option>
                                <option value="ZM">Zambia</option>
                                <option value="ZW">Zimbabwe</option>
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvPAN">
                        <label class="control-label col-md-3">
                            PAN
                        </label>
                        <div class="col-md-4">
                            <input id="txtPermanentAccountNumber" disabled="disabled" runat="server" type="text" class="form-control" name="permanent_account_number" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvSsn">
                        <label class="control-label col-md-3">SSN #</label>
                        <div class="col-md-4">
                            <input id="txtSsn" disabled="disabled" runat="server" type="text" class="form-control" name="ssn" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvIdentificationType">
                        <label class="control-label col-md-3">Identification Type</label>
                        <div class="col-md-4">
                            <select name="identification_type" disabled="disabled" runat="server" id="ddlIdentificationType" class="form-control select2">
                                <option value=""></option>
                                <option value="AADHAR_CARD">AADHAR CARD</option>
                                <option value="DRIVING_LICENSE">DRIVING LICENSE</option>
                                <option value="PASSPORT">PASSPORT</option>
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvIdentification">
                        <label class="control-label col-md-3">Identification #<span class="required" id="spnIdentification">* </span></label>
                        <div class="col-md-4">
                            <input id="txtIdentificationNumber" disabled="disabled" runat="server" type="text" class="form-control" name="identification_number" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvMobile">
                        <label class="control-label col-md-3">Mobile #</label>
                        <div class="col-md-4">
                            <input id="txtMobileNumber" disabled="disabled" type="number" runat="server" class="form-control" name="mobile_number" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvAddress">
                        <label class="control-label col-md-3">
                            Address
                        </label>
                        <div class="col-md-4">
                            <input id="txtAddress" disabled="disabled" type="text" runat="server" class="form-control" name="address" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvPin">
                        <label class="control-label col-md-3">
                            PIN
                        </label>
                        <div class="col-md-4">
                            <input id="txtPincodeNumber" disabled="disabled" runat="server" type="number" class="form-control" name="pincode_number" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvDoj">
                        <label class="control-label col-md-3">
                            Date Of Joining
                        </label>
                        <div class="col-md-4">
                            <input id="txtDateOfJoining" disabled="disabled" runat="server" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="date_Of_Joining" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvBeomeInsider">
                        <label class="control-label col-md-3">Date Becoming Insider</label>
                        <div class="col-md-4">
                            <input id="txtDateOfBecomingInsider" disabled="disabled" runat="server" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="date_Of_Becoming_Insider" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvCorpLoc">
                        <label class="control-label col-md-3">Location</label>
                        <div class="col-md-4">
                            <select disabled="disabled" runat="server" name="location" id="ddlLocation" class="form-control select2">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvCategory">
                        <label class="control-label col-md-3">Category</label>
                        <div class="col-md-4">
                            <select disabled="disabled" runat="server" name="category" id="ddlCategory" class="form-control select2">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvSubCategory">
                        <label class="control-label col-md-3">Sub category</label>
                        <div class="col-md-4">
                            <select name="sub_category" disabled="disabled" runat="server" id="ddlSubCategory" class="form-control"></select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvDepartment">
                        <label class="control-label col-md-3">Department</label>
                        <div class="col-md-4">
                            <select name="department" disabled="disabled" id="ddlDepartment" runat="server" class="form-control select2"></select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvDesignation">
                        <label class="control-label col-md-3">Designation</label>
                        <div class="col-md-4">
                            <select name="designation" disabled="disabled" id="ddlDesignation" runat="server" class="form-control select2">
                            </select>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvDin">
                        <label class="control-label col-md-3">DIN #</label>
                        <div class="col-md-4">
                            <input id="txtDinNumber" disabled="disabled" runat="server" type="text" class="form-control" name="din_number" />
                            <input id="txtD_ID" type="hidden" value="0" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvInstitution">
                        <label id="lblInstitution" class="control-label col-md-3">
                            Name of Educational Institution/<br />University of Graduation
                        </label>
                        <div class="col-md-4">
                            <input id="txtInstitution" disabled="disabled" runat="server" type="text" class="form-control" name="name_of_educational_institution_of_graduation" autocomplete="off" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvStream">
                        <label id="lblStream" class="control-label col-md-3">
                            Stream of Graduation
                        </label>
                        <div class="col-md-4">
                            <input id="txtStream" disabled="disabled" runat="server" type="text" class="form-control" name="stream_of_graduation" autocomplete="off" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group" id="dvEmployer">
                        <label id="lblEmployer" class="control-label col-md-3">
                            Details of the Past Employers
                        </label>
                        <div class="col-md-4">
                            <input id="txtEmployer" disabled="disabled" runat="server" type="text" class="form-control" name="details_of_past_employer" autocomplete="off" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group">
                        <label class="control-label col-md-3">
                            Last Modified
                        </label>
                        <div class="col-md-4">
                            <input id="lastModifiedOn" disabled="disabled" type="text" class="form-control" name="last_modified_on" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="form-group">
                        <label class="control-label col-md-3">
                            Version 
                        </label>
                        <div class="col-md-4">
                            <input id="version" disabled="disabled" type="hidden" class="form-control" name="version" />
                            <a class="btn btn-default" data-toggle="modal" data-target="#modalMyDetailsVersion" onclick="fnGetTransactionalInfoByVersion()">Version
                            </a>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalMyDetailsVersion" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 90%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">My Details</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll; max-height: 441px;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divMyDetailsVersion" class="col-md-10">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default">Close</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="js/Global.js" type="text/javascript"></script>
    <script src="js/MyDetails.js" type="text/javascript"></script>
</asp:Content>
