<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserDeclaration.aspx.cs" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" Inherits="ProcsDLL.InsiderTrading.UserDeclaration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/jquery-multi-select/css/multi-select.css" rel="stylesheet" type="text/css" />
    <link href="stylesheets/ej/web/default-theme/ej.web.all.min.css" rel="stylesheet" />
    <style>
        .form-wizard .steps > li > a.step > .desc, .form-wizard .steps > li > a.step > .number {
            display: inline-block !important;
            font-size: 12px !important;
            font-weight: 300 !important;
        }
        .requiredlbl {
            color: red !important;
        }
        .table > tbody > tr > td {
            vertical-align: middle !important;
        }
        .blink_me {
            animation: blinker 1s linear infinite;
        }
        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit " id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="icon-layers font-red"></i>
                            <span class="caption-subject font-red bold uppercase">
                                <span class="step-title" id="spnTitleLbl" runat="server" style="color:black !important;" />
                                <span class="step-title" id="spnTitle">Step 1 of 5 (PLEASE KEEP THE FOLLOWING DETAILS HANDY FOR FILLING UP THE FORM- PAN, 
                                    SPOUSE'S PAN, DEMAT ACCOUNT DETAILS AND NUMBER OF SHARES HELD IN NIIT Limited)
                                </span>
                            </span>
                        </div>
                        <div class="actions"></div>
                    </div>
                    <div class="portlet-body form">
                        <form class="form-horizontal" action="#" id="submit_form" method="post">
                            <input type="text" runat="server" id="hdnTaskFor"  style="display:none;" />
                            <input type="text" runat="server" id="hdnTaskId" style="display:none;" />
                            <input type="text" runat="server" id="hdnTaskStatus" style="display:none;" />
                            <input type="text" runat="server" id="hdnTaskFrm" style="display:none;" />
                            <input type="text" runat="server" id="HiddenTaskStatus" style="display:none;" />
                            <input type="text" runat="server" id="hdnFinalDeclared" style="display:none;" />
                            <input type="text" runat="server" id="hiddenTaskId" style="display:none;" />
                            <div class="form-wizard">
                                <div class="form-body">
                                    <ul class="nav nav-pills nav-justified steps">
                                        <li class="active" id="liPI">
                                            <a class="step">
                                                <span class="number">1 </span>
                                                <span class="desc">Personal Information </span>
                                            </a>
                                        </li>
                                        <li id="liRD">
                                            <a class="step">
                                                <span class="number">2 </span>
                                                <span class="desc">Add Relative Details </span>
                                            </a>
                                        </li>
                                        <li id="liDA">
                                            <a class="step">
                                                <span class="number">3 </span>
                                                <span class="desc">Add Demat Accounts </span>
                                            </a>
                                        </li>
                                        <li id="liIH">
                                            <a class="step">
                                                <span class="number">4 </span>
                                                <span class="desc">Initial Holdings </span>
                                            </a>
                                        </li>
                                        <li id="liCon">
                                            <a class="step">
                                                <span class="number">5 </span>
                                                <span class="desc">Confirmation</span>
                                            </a>
                                        </li>
                                    </ul>
                                    <div id="bar" class="progress progress-striped" role="progressbar">
                                        <div class="progress-bar progress-bar-success"></div>
                                    </div>
                                    <div class="tab-content">
                                        <div id="dvValidationMsg" class="alert alert-danger" style="display: none;">
                                            <button class="close" data-dismiss="alert"></button>
                                            Please fill in the required information
                                        </div>
                                        <div class="alert alert-success display-none">
                                            <button class="close" data-dismiss="alert"></button>
                                            Your form validation is successful!
                                        </div>
                                        <div class="tab-pane active" id="tab1">
                                            <h5 style="padding-left: 23px; color: blue;">
                                                <span style="color: red;">
                                                    <strong>Note : Please check your details. In case of any discrepancy, kindly contact Compliance Officer<a visible="false" runat="server" id="linkSupportMail" href="#">administrator.</a></strong>
                                                </span>
                                                <span id="spnRequest" style="float:right;padding-right:50px;display:none;">
                                                    <a id="aRaiseRequest" style="font-size: 11px;" href="javascript:fnRaiseRequest();" class="btn btn-outline green button-next">
                                                        Need to Change & Update your disclosures? <br /> Click here to raise a Change request to your Compliance Officer
                                                    </a>
                                                </span>
                                            </h5>
                                            <div class="form-group" id="dvCheckbox" style="display:none;">
                                                <label id="lblCheckbox" class="control-label col-md-3">
                                                    If there is no change in your declaration details, please click here to resubmit your last declaration.<span class="required" id="spnCheckbox"></span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input type="checkbox" id="UserCheck" name="UserTab" style="margin-top: 12px; width: 20px; height: 20px;" value="User"/>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvUsername">
                                                <label class="control-label col-md-3">Name<span class="required" id="spnUsername">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtUserName" type="text" runat="server" class="form-control" name="username" />
                                                    <%--<span class="help-block">Provide username</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvLoginId">
                                                <label class="control-label col-md-3">Username<span class="required" id="spnLoginId">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtLoginId" runat="server" type="text" class="form-control" name="login_id" />
                                                    <input id="txtSpousePANMandatory" runat="server" type="hidden" />
                                                    <input id="txtMFRMandatory" runat="server" type="hidden" />
                                                    <input id="txtRelativeEmail" runat="server" type="hidden" />
                                                    <input id="txtRelativeAddress" runat="server" type="hidden" />
                                                    <%--<span class="help-block">Provide Login ID</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvPersonalEmail">
                                                <label class="control-label col-md-3">Personal Email<span class="required" id="spnPersonalEmail">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtPersonalEmail" type="text" class="form-control" name="personal_email" />
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvMobile">
                                                <label class="control-label col-md-3">Mobile #<span class="required" id="spnMobile">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtMobileNumber" type="text" runat="server" class="form-control mobile" maxlength="15" name="mobile_number" />
                                                    <%--<span class="help-block">Provide mobile #</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvAddress">
                                                <label class="control-label col-md-3">
                                                    Address<span class="required" id="spnAddress">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtAddress" type="text" runat="server" class="form-control" name="address" />
                                                    <%--<span class="help-block">Provide address</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvPin">
                                                <label class="control-label col-md-3">
                                                    PIN / ZIP Code<span class="required" id="spnPin">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtPincodeNumber" runat="server" type="text" class="form-control" maxlength="6" name="pincode_number" />
                                                    <%--<span class="help-block">Provide pin code</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvResidentType">
                                                <label class="control-label col-md-3">
                                                    Resident Type<span class="required" id="spnResidentType">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="resident_type" runat="server" id="ddlResidentType" class="form-control">
                                                        <option value=""></option>
                                                        <option value="NRI">NRI</option>
                                                        <option value="FOREIGN_NATIONAL">FOREIGN NATIONAL</option>
                                                        <option value="INDIAN_RESIDENT">INDIAN RESIDENT</option>
                                                    </select>
                                                    <%--<span class="help-block">Select Resident Type</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvCountry">
                                                <label class="control-label col-md-3">
                                                    Country of Residence<span class="required" id="spnCountry">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="country" runat="server" id="country" class="form-control">
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
                                            <div class="form-group" id="dvPAN">
                                                <label class="control-label col-md-3">
                                                    PAN<span class="required" id="spnPAN">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtPermanentAccountNumber" runat="server" type="text" class="form-control" name="permanent_account_number" oninput="javascript:if(this.value.length>this.maxLength) this.value=this.value.slice(0,this.maxLength).toUpperCase(); else this.value=this.value.toUpperCase();" maxlength="10" />
                                                    <%--<span class="help-block">Provide PAN</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvIdentificationType">
                                                <label class="control-label col-md-3">Identification Type<span class="required" id="spnIdentificationType">* </span></label>
                                                <div class="col-md-4">
                                                    <select name="identification_type" runat="server" id="ddlIdentificationType" class="form-control">
                                                        <option value=""></option>
                                                        <option value="AADHAR_CARD">AADHAR CARD</option>
                                                        <option value="DRIVING_LICENSE">DRIVING LICENSE</option>
                                                        <option value="PASSPORT">PASSPORT</option>
                                                    </select>
                                                    <%--<span class="help-block">Select identification type</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvIdentification">
                                                <label class="control-label col-md-3">Identification #<span class="required" id="spnIdentification">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtIdentificationNumber" runat="server" type="text" class="form-control" name="identification_number" />
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvMaritalStatus" style="display:none;">
                                                <label class="control-label col-md-3">Do you have a spouse?<span class="required" id="spnMarriedStatus">*  </span></label>
                                                <div class="col-md-4">
                                                    <label class="radio-inline">
                                                        <input id="YesMarried" value="Yes" type="radio" name="radioMarried" />Yes
                                                    </label>
                                                    <label class="radio-inline">
                                                        <input id="NoMarried" value="No" type="radio" name="radioMarried" />No
                                                    </label>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvParentCompany">
                                                <label class="control-label col-md-3">Company<span class="required" id="spnParentCompany">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtParentCompanyName" runat="server" type="text" class="form-control" name="parent_company_name" />
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvEmail">
                                                <label class="control-label col-md-3">Email<span class="required" id="spnEmail">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtEmailId" runat="server" type="text" class="form-control" name="email" />
                                                    <%--<span class="help-block">Provide email</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvCompany">
                                                <label class="control-label col-md-3">Subsidiary<span class="required" id="spnCompany">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtCompanyName" runat="server" type="text" class="form-control" name="company_name" />
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvRole">
                                                <label class="control-label col-md-3">Role<span class="required" id="spnRole">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtRole" runat="server" type="text" class="form-control" name="role" />
                                                    <%--<span class="help-block">Provide role</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvEmployeeId">
                                                <label class="control-label col-md-3">
                                                    Employee ID<span class="required" id="spnEmployeeId">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtEmployeeId" runat="server" type="text" class="form-control" name="employee_id" />
                                                    <%--<span class="help-block">Provide Employee ID</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvEsopOrEquity" style="display: none">
                                                <label class="control-label col-md-3">
                                                    Do you hold ESOP or Equity in the company ?<span class="required" id="spnEsopOrEquity">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input type="radio" id="txtHoldsEsopOrEquity" name="holdsEsopOrEquity" value="Yes" />
                                                    <label for="txtHoldsEsopOrEquity">Yes</label>
                                                    <input type="radio" id="txtNotHoldsEsopOrEquity" name="holdsEsopOrEquity" checked="checked" value="No" />
                                                    <label for="txtNotHoldsEsopOrEquity">No</label>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvApplicationPendingForPan" style="display: none">
                                                <label class="control-label col-md-3">
                                                    Is PAN/DEMAT Account Number Available?<span class="required" id="spnApplicationPendingForPan">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input type="radio" id="txtApplicationPendingForPAN" name="ApplicationPending" value="Yes" />
                                                    <label for="txtApplicationPendingForPAN">No</label>
                                                    <input type="radio" id="txtApplicationNotPendingForPAN" name="ApplicationPending" checked="checked" value="No" />
                                                    <label for="txtApplicationNotPendingForPAN">Yes</label>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvSsn">
                                                <label class="control-label col-md-3">SSN #<span class="required" id="spnSsn">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtSsn" runat="server" type="text" class="form-control" name="ssn" />
                                                    <%--<span class="help-block">Provide SSN</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvDoj">
                                                <label class="control-label col-md-3">
                                                    Date of Joining<span class="required" id="spnDoj">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtDateOfJoining" runat="server" type="text" class="form-control" name="date_Of_Joining" />
                                                    <%--<span class="help-block">Provide date of joining</span>--%>
                                                </div>
                                                <div class="col-md-1">
                                                    <span><b>DD/MM/YYYY</b></span>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvBeomeInsider">
                                                <label class="control-label col-md-3">Date of Becoming Insider<span class="required" id="spnBeomeInsider">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtDateOfBecomingInsider" runat="server" type="text" class="form-control" name="date_Of_Becoming_Insider" />
                                                    <span style="color: red; display: none;">(Date of joining or promotion to KMP / Director / DP)</span>
                                                    <%--<span class="help-block">Provide date of become insider</span>--%>
                                                </div>
                                                <div class="col-md-1">
                                                    <span><b>DD/MM/YYYY</b></span>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvCorpLoc">
                                                <label class="control-label col-md-3">Office Location<span class="required" id="spnCorpLoc">* </span></label>
                                                <div class="col-md-4">
                                                    <select runat="server" name="location" id="ddlLocation" class="form-control select2" data-placeholder="Select a Location">
                                                    </select>
                                                    &nbsp;
                                                </div>
                                                <div class="col-md-1">
                                                    <a data-toggle="tooltip" title="Click to add new location" class="btn green" onclick="javascript:fnAddLocation();">
                                                        <i class="fa fa-plus"></i>
                                                    </a>
                                                    <%--<button id="btnAddLocation"  class="btn green" onclick="javascript:fnAddLocation();"  data-target="#stack1Location" data-toggle="modal"><i class="fa fa-plus"></i></button>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvCategory">
                                                <label class="control-label col-md-3">Role</label>
                                                <div class="col-md-4">
                                                    <select runat="server" name="category" id="ddlCategory" class="form-control select2" data-placeholder="Select a Category">
                                                    </select>
                                                    <%--<span class="help-block">Select category</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvSubCategory">
                                                <label class="control-label col-md-3">Sub category<span class="required" id="spnSubCategory">* </span></label>
                                                <div class="col-md-4">
                                                    <select name="sub_category" runat="server" id="ddlSubCategory" class="form-control" data-placeholder="Select a Sub-Category"></select>
                                                    <%--<span class="help-block">Select sub category</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvDepartment">
                                                <label class="control-label col-md-3">Department<span class="required" id="spnDepartment">* </span></label>
                                                <div class="col-md-4">
                                                    <select name="department" id="ddlDepartment" runat="server" class="form-control select2" data-placeholder="Select a Department"></select>
                                                    <%--<span class="help-block">Select department</span>--%>
                                                </div>
                                                <div class="col-md-1">
                                                    <a class="btn green" data-toggle="tooltip" title="Click to add new department" onclick="javascript:fnAddDepartment();">
                                                        <i class="fa fa-plus"></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvDesignation">
                                                <label class="control-label col-md-3">Designation<span class="required" id="spnDesignation">* </span></label>
                                                <div class="col-md-4">
                                                    <select name="designation" id="ddlDesignation" runat="server" class="form-control select2" data-placeholder="Select a Designation" onchange="javascript:ddlDesignation_onChange();">
                                                    </select>&nbsp;
                                                    <%--<span class="help-block">Select designation</span>--%>
                                                </div>
                                                <div class="col-md-1">
                                                    <a class="btn green" data-toggle="tooltip" title="Click to add new designation" onclick="javascript:fnAddDesignation();">
                                                        <i class="fa fa-plus"></i>
                                                    </a>
                                                    <%--<button id="Button1"  class="btn green" onclick="javascript:fnAddDesignation();" data-target="#stack1Designation" data-toggle="modal"> <i class="fa fa-plus"></i></button>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvDin">
                                                <label class="control-label col-md-3">CIN/DIN<span class="required" id="spnDin">* </span></label>
                                                <div class="col-md-4">
                                                    <input id="txtDinNumber" runat="server" type="text" class="form-control" name="din_number" />
                                                    <%--<span class="help-block">Provide DIN</span>--%>
                                                    <input id="txtD_ID" type="hidden" value="0" />
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvInstitution">
                                                <label id="lblInstitution" class="control-label col-md-3">
                                                    Name of Educational Institution/<br />
                                                    University of Graduation<span class="required" id="spnInstitution">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <textarea id="txtInstitution" class="form-control" name="name_of_educational_institution_of_graduation"></textarea>
                                                    <%--<span class="help-block">Provide Educational Institution of Graduation</span>--%>
                                                    <span style="color: red; display: none">All Educational Qualifications & Institutions – to be mentioned in a descending order separated by a comma (,) starting from your Graduation </span>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvStream">
                                                <label id="lblStream" class="control-label col-md-3">
                                                    Stream of Graduation<span class="required" id="spnStream">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtStream" runat="server" type="text" class="form-control" name="stream_of_graduation" autocomplete="off" />
                                                    <%--<span class="help-block">Provide Stream of Graduation</span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group" id="dvEmployer">
                                                <label id="lblEmployer" class="control-label col-md-3">
                                                    Names of the Past Employers<span class="required" id="spnEmployer">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtEmployer" runat="server" type="text" class="form-control" name="details_of_past_employer" autocomplete="off" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab2">
                                            <h5 style="padding-left: 23px; color: blue;">Please provide dependent relative(s) detail. Material financial relationship information if you 
                                                have paid more than 25% of your Annual Income to any person.
                                            </h5>
                                            <div style="margin: 0 20px" class="actions">
                                                <a class="btn btn-default display-none" data-toggle="modal" data-target="#modalAddRelativeDetail" onclick="fnOpenModalAddRelativeDetail();">
                                                    <i class="fa fa-plus"></i>Add
                                                </a>
                                                <input id="txtSpouseRelationId" style="display: none;" runat="server" type="text" class="form-control" />
                                                <input id="txtSpouseRelationNm" style="display: none;" runat="server" type="text" class="form-control" />
                                                <input id="txtMFRId" style="display: none;" runat="server" type="text" class="form-control" />
                                                <input id="txtMFRNm" style="display: none;" runat="server" type="text" class="form-control" />
                                                <br />
                                                <br />

                                                <table class="table table-striped table-hover table-bordered" id="tbl-Relative-setup" style="width: 100%;">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display: none">RELATIVE ID</th>
                                                            <th style="display: none">RELATION ID</th>
                                                            <th style="display: none">Mandatory</th>
                                                            <th style="width: 20%">RELATION</th>
                                                            <th style="width: 10%">STATUS</th>
                                                            <th>Relative Name</th>
                                                            <th>Relative Email</th>
                                                            <th>Identification Type</th>
                                                            <th>Identification No #</th>
                                                            <th>ADDRESS</th>
                                                            <th>PHONE #</th>                                                            
                                                            <th>Please check
                                                                <br />
                                                                if working at
                                                                <br />
                                                                <span id="spnCompanyName" runat="server"></span>/Subsidiary</th>
                                                            <td style="width:8%"></td>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdRelativeList">
                                                    </tbody>
                                                </table>
                                            </div>
                                            <footer>
                                                <h5 style="margin: 0 20px 10px; color: red">Important Note-</h5>
                                                <ol style="margin-right: 20px; color: red">
                                                    <li>“Immediate Relative" means a spouse of a person, and includes parent, sibling, and child of such person or of the spouse, any of whom is either dependent financially on such person, or consults such person in taking decisions relating to trading in securities.</li>
                                                    <li>“Material Financial Relationship” means a relationship where a person (including relatives) is in receipt of any kind of payment by way of a loan or gift from the Designated Person during the immediately preceding twelve months, cumulatively equivalent to at least 25% of the Designated Person’s annual income; but shall exclude relationships in which the payment is based on arm’s length transactions.</li>
                                                </ol>

                                            </footer>
                                        </div>
                                        <div class="tab-pane" id="tab3">
                                            <h5 style="padding-left: 23px; color: blue;">Kindly provide details of DEMAT account(s) of self and relative(s), if any.
                                            </h5>

                                            <div style="margin-left: 20px" class="actions">
                                                <a class="btn btn-default" data-toggle="modal" data-target="#modalAddDematDetail">
                                                    <i class="fa fa-plus"></i>Add
                                                </a>
                                                <br />
                                                <br />
                                                <table class="table table-striped table-hover table-bordered" id="tbl-Demat-setup" style="width: 1000px">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display: none;">DEMAT ACCOUNT ID</th>
                                                            <th style="display: none;">DEMAT ACCOUNT DETAIL FOR ID</th>
                                                            <%--<th>DEMAT ACCOUNT DETAIL FOR</th>--%>
                                                            <th>FOR</th>
                                                            <%--  <th>DEPOSITORY NAME</th>--%>
                                                            <th>DEPOSITORY</th>
                                                            <th>CLIENT ID</th>
                                                            <%--<th>DEPOSITORY PARTICIPANT NAME</th>--%>
                                                            <th>PARTICIPANT</th>
                                                            <%--<th>DEPOSITORY PARTICIPANT ID</th>--%>
                                                            <th>PARTICIPANT ID</th>
                                                            <th style="display: none">TRADING MEMBER ID</th>
                                                            <%--<th>DEMAT ACCOUNT NO</th>--%>
                                                            <th>DEMAT</th>
                                                            <th>STATUS</th>
                                                            <th>Edit</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdDematList">
                                                    </tbody>
                                                </table>
                                            </div>
                                            <footer>
                                                <h5 style="margin: 0 20px 10px; color: red">Important Note-</h5>
                                                <ol style="margin-right: 20px; color: red">
                                                    <li>Please add all DEMAT accounts for yourself and your immediate relatives. Select "Not Applicable" in case no DEMAT accounts are available</li>
                                                    <li>Maximum 20 DEMAT accounts per PAN can be added.</li>
                                                </ol>
                                            </footer>
                                        </div>
                                        <div class="tab-pane" id="tab4">
                                            <h5 style="padding-left: 23px; color: blue;">Add Initial Holding Declaration</h5>
                                            <div style="margin-left: 20px" class="actions">
                                                <a class="btn btn-default" data-toggle="modal" data-target="#modalAddInitialHoldingDeclarations">
                                                    <i class="fa fa-plus"></i>Add Demat Holding
                                                </a>
                                                <a id="BtnOtherHolding" runat="server" class="btn btn-default" data-toggle="modal" data-target="#modalAddPhysicalShares">
                                                    <i class="fa fa-plus"></i>Add Physical Holding
                                                </a>
                                                <br />
                                                <br />
                                                <p>Holding in Equity</p>
                                                <table class="table table-striped table-hover table-bordered" id="tbl-InitialHolding-setup" style="width: 100%">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display:none;">RESTRICTED COMPANY ID</th>
                                                            <th style="display:none;">COMPANY</th>
                                                            <th style="display:none;">SECURITY TYPE ID</th>
                                                            <th style="display:none;">SECURITY TYPE</th>
                                                            <th style="display:none;">RELATIVE ID</th>
                                                            <th>FOR</th>
                                                            <th style="display:none;">DEMAT ACCOUNT ID</th>
                                                            <th>DEMAT ACCOUNT NO</th>
                                                            <th>NUMBER OF SECURITIES</th>
                                                            <th style="display: none;">PAN</th>
                                                            <th style="display: none;">TRADING MEMBER ID</th>
                                                            <th>Equity Shares held as on 1st APRIL <span id="fyinitial"></span></th>
                                                            <th>Equity Shares held as on 31st MARCH <span id="fylast"></span></th>
                                                            <th>Total Equity Shares bought during this period</th>
                                                            <th>Total Value of Equity Shares bought during this period (INR &#8377;)</th>
                                                            <th>Total Equity Shares Sold during this period</th>
                                                            <th>Total Value of Equity Shares Sold during this period (INR &#8377;)</th>
                                                            <th>Edit</th>
                                                            <th style="display: none;">INITIAL HOLDING ID</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdInitialDeclaration">
                                                    </tbody>
                                                </table>
                                                <br />
                                                <br />
                                                 <div id="divTransactionHistory" runat="server">
                                                <p>Holding Transaction Details</p>  
                                                <table class="table table-striped table-hover table-bordered" id="tbltransactionhistory-setup" style="width: 100%;">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display: none">Transaction ID</th>
                                                            <th>Transaction By</th>
                                                            <th>Trans date</th>
                                                            <th>Type</th>
                                                            <th>Quantity</th>
                                                            <th>Value</th>
                                                            <th>Action</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdTransactionHistoryList">
                                                    </tbody>
                                                </table>
                                                </div>
                                                <br />
                                                <p>Holding in Physical</p>
                                                <table class="table table-striped table-hover table-bordered" id="tbl-PhysicalShare-setup" style="width: 1000px;">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display: none;">RESTRICTED COMPANY ID</th>
                                                            <th>COMPANY</th>
                                                            <th style="display: none;">SECURITY TYPE ID</th>
                                                            <th>SECURITY TYPE</th>
                                                            <th style="display: none;">RELATIVE ID</th>
                                                            <th>FOR</th>
                                                            <th>Quantity</th>
                                                            <th>DEMAT ACCOUNT NO</th>
                                                            <th style="display: none;">PAN</th>
                                                            <th>Edit</th>
                                                            <th style="display: none;">INITIAL HOLDING ID</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdPhysicalDeclaration">
                                                    </tbody>
                                                </table>
                                                    
                                                <br />
                                                <br />
                                                <div id="divDebtSecurity" style="display:none">
                                                <p>Holding in Debt Security</p>
                                                <table class="table table-striped table-hover table-bordered" id="tbl-debtSecurity-setup" style="width: 100%;">
                                                    <thead class="text-uppercase">
                                                        <tr>
                                                            <th style="display: none">ACCOUNT ID</th>
                                                            <th style="display: none">RELATIVE ID</th>
                                                            <th>Relative Name</th>
                                                            <th>DEMAT Account</th>
                                                            <th>Current Holding</th>
                                                            <th>Equity Shares held as on 1st April <span id="fyinitial2"></span></th>
                                                            <th>Equity Shares held as on 31st March <span id="fylast2"></span></th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdDebtSecurityList">
                                                    </tbody>
                                                </table>
                                                    </div>
                                                <br />
                                            </div>
                                            <footer>
                                                <h5 style="margin: 0 20px 10px; color: red">Important Note-</h5>
                                                <ol style="margin-right: 20px; color: red">
                                                    <li>The Holding Details mentioned above are as per the Company RTA Records which were last updated in system on - </li>
                                                    <li>Please use the edit icon in case of any variance or update required in holding details.</li>
                                                </ol>

                                            </footer>
                                        </div>
                                        <div class="tab-pane" id="tab5">
                                            <h5 style="padding-left: 23px; color: blue;">Submit Declaration (Kindly preview your form before submitting your declaration).</h5>
                                            <table class="table table-striped table-hover table-bordered" style="margin-left: 23px; width: 90%!important;">

                                                <tbody>
                                                    <tr>
                                                        <td>You are about to submit your final disclosure
                                                        </td>
                                                        <td><a id="LinkButtonPreviewForm" class="btn btn-sm blue" href="#" onclick="javascript:fnPreviewDeclarationForm();">Preview Form</a></td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td>Mail To</td>
                                                        <td id="tdMailToUserName"></td>

                                                    </tr>
                                                    <tr>
                                                        <td>Mail To</td>
                                                        <td>Compliance Officer(<label runat="server" id="lblComplianceEmailId" style="color:darkblue"></label>)</td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td>Mail To</td>
                                                        <td id="tdMailTo"></td>
                                                    </tr>
                                                    <tr>
                                                        <td>Mail From</td>
                                                        <td id="tdMailFromUserName"></td>
                                                    </tr>
                                                    <tr style="display: none;">
                                                        <td>Mail From</td>
                                                        <td id="tdMailFrom"></td>
                                                    </tr>

                                                    <tr>
                                                        <td>Subject</td>
                                                        <td id="tdSubject">Declaration under the Insider Trading Code</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <label style="padding-left: 23px;">Please review the details below and accept the Insider Trading Code for submission.</label>
                                            <table class="table table-striped table-hover table-bordered" style="margin-left: 23px; width: 90%!important;">
                                                <thead class="text-uppercase">
                                                    <tr>
                                                        <th>For</th>
                                                        <th>Company</th>
                                                        <th>Security Type</th>
                                                        <th>Demat Account No</th>
                                                        <th>Number Of Securities</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="templateBodyInitialHoldingDeclaration">
                                                </tbody>
                                            </table>
                                            <label style="padding-left: 23px;">Policy Document</label>
                                            <table class="table table-striped table-hover table-bordered" style="margin-left: 23px; width: 1000px;">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <div id="viewer" style="height: 600px; width: 950px; min-height: 404px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <p id="DeclarationHerebyContent" runat="server"></p>
                                                            <%--<input type="checkbox" id="inAcceptTermsAndConditions" />&nbsp;<span>I hereby declare that I accept the terms and conditions of this policy.</span>&nbsp;<a href="#" id="aInAcceptTermsAndConditions" target="_blank">Please click here to read the Insider Trading Policy</a></td>--%>
                                                            <input type="checkbox" id="inAcceptTermsAndConditions" />&nbsp;<span id="txtTermsAndConditions">I hereby declare that I accept the terms and conditions of this policy.</span>

                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-0 col-md-9" style="margin-left: 20px;">
                                            <a href="javascript:fnBack();" class="btn default button-previous">
                                                <i class="fa fa-angle-left"></i>Back
                                            </a>
                                            <a id="aSavenContinue" href="javascript:fnSaveScreenInformation();" class="btn btn-outline green button-next">Save & Continue
                                                <i class="fa fa-angle-right"></i>
                                            </a>
                                            <a id="aSubmitYourDeclaration" style="pointer-events: none; color: #666; background-color: #e1e5ec; border-color: #e1e5ec; display: none;" disabled="true" href="javascript:fnSendEmailNoticeConfirmation();" class="btn green button-submit">Submit Your Declaration 
                                                <i class="fa fa-check"></i>
                                            </a>
                                            <span class="text-danger blink_me" id="spnblinkpreview" style="display: none;">Kindly preview your form before submitting your declaration.</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="mdlRequest" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width:700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <h4 class="modal-title">Request to modify declaration</h4>
                </div>
                <div class="modal-body">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divRequestReason" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblReqRemarks" class="col-md-3 control-label">
                                        <span id="spReqRemarks">
                                            Reasons for request<span class="required">* </span>
                                        </span>
                                    </label>
                                    <div class="col-md-8" style="margin-left:53px;">
                                        <textarea id="txtReqReason" class="form-control"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalRaiseRequest();">Close</button>
                    <button id="btnRaiseReq" type="button" class="btn green" onclick="javascript:fnSubmitRequest();">Submit</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAddRelativeDetail" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalAddRelativeDetail();"></button>
                    <h4 class="modal-title">Relative Detail</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                    </div>
                </div>
                <div class="modal-footer">
                    </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAddDematDetail" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalAddDematDetail();"></button>
                    <h4 class="modal-title">Demat Detail</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divDematAccountDetailsFor" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblDematAccountDetailsFor" style="text-align: left" class="col-md-4 control-label">Demat Account Details For<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlDematAccountDetailsFor" class="form-control" onchange="javascript:fnRemoveClass(this,'DematAccountDetailsFor');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDepositoryName" style="text-align: left" class="col-md-4 control-label">Depository Name<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlDepositoryName" class="form-control" onchange="javascript:fnRemoveClass(this,'DepositoryName');">
                                            <option value=""></option>
                                            <option value="NotApplicable">Not Applicable</option>
                                            <option value="CDSL">CDSL</option>
                                            <option value="NSDL">NSDL</option>
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDepositoryParticipantName" style="text-align: left" class="control-label col-md-4">
                                        Depository Participant Name
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtDepositoryParticipantName" type="text" class="form-control" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="254" onkeypress="javascript:fnRemoveClass(this,'DepositoryParticipantName');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDepositoryParticipantId" style="text-align: left" class="col-md-4 control-label">Depository Participant Id<span class="required"> * </span></label>

                                    <div class="col-md-8">
                                        <div class="input-group">
                                            <span id="spNsdlLabel" class="input-group-addon"></span>
                                            <input id="txtDepositoryParticipantId" type="text" class="form-control number" autocomplete="off" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblClientId" style="text-align: left" class="col-md-4 control-label">Client Id<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtClientId" type="text" class="form-control number" maxlength="8" onkeypress="javascript:fnRemoveClass(this,'ClientId');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDematAccountNumber" style="text-align: left" class="col-md-4 control-label">Demat Account Number<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <div class="input-group">
                                            <span id="spNsdlDematLabel" class="input-group-addon"></span>
                                            <input id="txtDematAccountNumber" type="text" class="form-control" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="254" readonly="readonly" onkeypress="javascript:fnRemoveClass(this,'DematAccountNumber');" autocomplete="off" />
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group" style="display: none;">
                                    <label id="lblTradingMemberId" style="text-align: left" class="col-md-4 control-label">Trading MemberId Id<%--<span class="required"> * </span>--%></label>
                                    <div class="col-md-8">
                                        <input id="txtTradingMemberId" type="text" class="form-control" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="254" onkeypress="javascript:fnRemoveClass(this,'TradingMemberId');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblStatusDemat" class="control-label col-md-4">
                                        Demat Status<span class="required">* </span>
                                    </label>
                                    <div class="col-md-8">
                                        <select name="Status_Demat" id="ddlStatusDemat" class="form-control" onchange="javascript:fnRemoveClass(this,'StatusDemat');">
                                            <option value=""></option>
                                            <%--<option value="NotApplicable">Not Applicable</option>--%>
                                            <option value="ACTIVE">ACTIVE</option>
                                            <option value="INACTIVE">INACTIVE</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-8" style="text-align:left !important;">
                        <img src="../assets/images/Picture2.png" style="width:350px;height:80px" />
                    </div>
                    <div class="col-md-4">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalAddDematDetail();">Close</button>
                    <button id="btnSaveDematDetail" type="button" class="btn green" onclick="javascript:fnAddDematDetail();">Add</button>
                        </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAddInitialHoldingDeclarations" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalAddInitialHoldingDeclarations();"></button>
                    <h4 class="modal-title">Initial Holding Declaration</h4>
                </div>
                <div class="modal-body" style="overflow-y:scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divInitialHoldingDeclaration" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblRestrictedCompanies" style="text-align: left" class="col-md-4 control-label">Company<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlRestrictedCompaniesX" runat="server" class="form-control" onchange="javascript:fnRemoveClass(this,'RestrictedCompanies');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblSecurityType" style="text-align: left" class="col-md-4 control-label">Security Type<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlSecurityType" runat="server" class="form-control" onchange="javascript:fnRemoveClass(this,'SecurityType');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblFor" style="text-align: left" class="col-md-4 control-label">For<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlFor" class="form-control" onchange="javascript:fnRemoveClass(this,'For');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblPan" style="text-align: left" class="control-label col-md-4">
                                        PAN
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtPan" type="text" class="form-control" readonly="readOnly" onkeypress="javascript:fnRemoveClass(this,'Pan');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDematAccNo" style="text-align: left" class="col-md-4 control-label">Demat Account No<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlDematAccNo" class="form-control" onchange="javascript:fnRemoveClass(this,'DematAccNo');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group" style="display: none">
                                    <label id="lblTradingMemId" style="text-align: left" class="col-md-4 control-label">Trading MemberId Id</label>
                                    <div class="col-md-8">
                                        <input id="txtTradingMemId" type="text" class="form-control" readonly="readOnly" onkeypress="javascript:fnRemoveClass(this,'TradingMemId');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblNumberOfSecurities" style="text-align: left" class="col-md-4 control-label">Equity Shares held as on Date<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtNumberOfSecurities" type="number" max="10" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'NumberOfSecurities');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblApril" style="text-align: left" class="col-md-4 control-label">Equity Shares held as on 1st April <span id="fyinitial3"></span><span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtApril" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'April');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblMarch" style="text-align: left" class="col-md-4 control-label">Equity Shares held as on 31st March <span id="fylast3"></span><span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtMarch" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'March');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblTotalBuy" style="text-align: left" class="col-md-4 control-label">Total Equity Shares bought during this period<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtTotalBuy" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'TotalBuy');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblTotalBuyValue" style="text-align: left" class="col-md-4 control-label">Total Value of Equity Shares bought during this period<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtTotalBuyValue" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'TotalBuyValue');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblTotalSell" style="text-align: left" class="col-md-4 control-label">Total Equity Shares Sold during this period<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtTotalSell" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'TotalSell');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblTotalSellValue" style="text-align: left" class="col-md-4 control-label">Total Value of Equity Shares Sold during this period<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtTotalSellValue" type="text" class="form-control number" onkeypress="javascript:fnRemoveClass(this,'TotalSellValue');" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalAddInitialHoldingDeclarations();">Close</button>
                    <button id="btnSaveInitialHoldingDeclaration" type="button" class="btn green" onclick="javascript:fnAddInitialHoldingDeclarationDetail();">Add</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalAddPhysicalShares" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalAddInitialHoldingDeclarationsPhysical();"></button>
                    <h4 class="modal-title">Physical Shares Declaration</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divPhysicalShares" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblRestrictedCompaniesPhysical" style="text-align: left" class="col-md-4 control-label">Company<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlRestrictedCompaniesPhysical" runat="server" class="form-control" onchange="javascript:fnRemoveClass(this,'RestrictedCompaniesPhysical');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblOtherSecurityType" style="text-align: left" class="col-md-4 control-label">Security Type<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlOtherSecurityType" runat="server" class="form-control" onchange="javascript:fnRemoveClass(this,'OtherSecurityType');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblForPhysical" style="text-align: left" class="col-md-4 control-label">For<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlForPhysical" class="form-control" onchange="javascript:fnRemoveClass(this,'ForPhysical');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblPanPhysical" style="text-align: left" class="control-label col-md-4">
                                        PAN
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtPanPhysical" type="text" class="form-control" readonly="readOnly" onkeypress="javascript:fnRemoveClass(this,'PanPhysical');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblPhysicalDematAccNo" style="text-align: left" class="col-md-4 control-label">Folio #</label>
                                    <div class="col-md-8">
                                        <input type="text" id="ddlPhysicalDematAccNo" class="form-control" />
                                        <%--<select id="ddlPhysicalDematAccNo" class="form-control" onchange="javascript:fnRemoveClass(this,'PhysicalDematAccNo');"></select>--%>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblNumberOfSecuritiesPhysical" style="text-align: left" class="col-md-4 control-label">Quantity<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtNumberOfSecuritiesPhysical" type="number" max="10" class="form-control" onkeypress="javascript:fnRemoveClass(this,'NumberOfSecuritiesPhysical');" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalAddInitialHoldingDeclarationsPhysical();">Close</button>
                    <button id="btnSaveInitialHoldingDeclarationPhysical" type="button" class="btn green" onclick="javascript:fnAddInitialHoldingDeclarationDetailPhysical();">Add</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalDeleteRelativeDetail" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to delete relative information?</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtDeleteRelativeDetailId" type="hidden" value="" />
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalDeleteRelativeDetail">NO</button>
                    <input value="YES" id="btnDeleteRelativeDetail" data-dismiss="modal" class="btn red" onclick="fnDeleteRelativeDetailModal()" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalDeleteDematDetail" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to delete demat information?</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtDeleteDematDetailId" type="hidden" value="" />
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalDeleteDematDetail">NO</button>
                    <input value="YES" id="btnDeleteDematDetail" data-dismiss="modal" class="btn red" onclick="fnDeleteDematDetailModal()" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalDeleteInitialHoldingDeclarations" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to delete initial holding information?</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtDeleteInitialHoldingDetailId" type="hidden" value="" />
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalDeleteInitialHoldingDeclarations">NO</button>
                    <button type="button" class="btn red" id="btnDeleteInitialHoldingDetail" data-dismiss="modal" onclick="fnDeleteInitialHoldingDetailModal()">YES</button>
                    
                    <%--<input type="button"  value="YES" id="btnDeleteInitialHoldingDetail" data-dismiss="modal" class="btn red" onclick="fnDeleteInitialHoldingDetailModal()" />--%>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade in" id="modalDeletePhysicalHoldingDeclarations" tabindex="-1" role="dialog" aria-hidden="True" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 50%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="True"></button>
                    <h4 class="modal-title"><b>Are you sure you want to delete initial holding information?</b></h4>
                </div>
                <div class="modal-footer">
                    <input id="txtDeletePhysicalHoldingDetailId" type="hidden" value="" />
                    <button type="button" class="btn dark btn-outline" data-toggle="modal" data-target="#modalDeletePhysicalHoldingDeclarations">NO</button>
                    <input value="YES" id="btnDeletePhysicalHoldingDetail" data-dismiss="modal" class="btn red" onclick="fnDeleteInitialHoldingDetailPhysicalModal()" type="submit" />
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="stack1Department" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">Department SetUp</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4" id="lblDepartment" style="margin-top: 5px;">Department</div>
                        <div class="col-md-6">
                            <input id="txtDepartmentName" class="form-control" type="text" />
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
    <div class="modal fade" id="stack1Designation" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title">Designation SetUp</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4" style="margin-top: 5px;">Designation</div>
                        <div class="col-md-6">
                            <input id="txtDesignationName" class="form-control" type="text" data-tabindex="1" />
                            <input class="form-control" id="txtDesignationKey" type="text" style="display: none;" />
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                    <br />
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn btn-outline dark" onclick="javascript:fnCloseModal();">Close</button>
                    <button id="btnSaveDesig" type="button" data-dismiss="modal" class="btn green" onclick="javascript:fnSaveDesignation();">Save</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <div class="modal fade" id="stack1Location" tabindex="-1" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModal();"></button>
                    <h4 class="modal-title caption-subject bold uppercase font-red-sunglo">Add Location</h4>
                </div>
                <div class="modal-body">

                    <div class="portlet light bordered">
                        <div class="portlet-body form">
                            <form class="form-horizontal" runat="server" role="form">
                                <div class="form-body modal-fixheight">
                                    <div class="form-group">
                                        <label id="lblRole" style="text-align: left" class="col-md-4 control-label">Location<span class="required"> * </span></label>
                                        <div class="col-md-8">
                                            <input id="txtlocationname" class="form-control form-control-inline" placeholder="Enter Location" size="16" type="text" value="" autocomplete="off" />
                                            <input id="idlocation" class="form-control form-control-inline" size="16" type="text" value="" autocomplete="off" style="display: none" />
                                        </div>
                                    </div>

                                    <br />


                                </div>
                                <div class="form-actions">
                                    <div class="row" style="text-align: center">
                                        <div class="col-md-offset-4 col-md-12">
                                            <button id="btnCancel1" type="button" data-dismiss="modal" class="btn default">Close</button>
                                            <button id="btnSaveL" type="button" class="btn green" onclick="javascript:fnSaveLocation();">Save</button>
                                        </div>
                                    </div>
                                </div>

                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/select2/js/select2.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-select2.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <script src="js/ej/ej.web.all.min.js"></script>
    <script src="js/Global.js"></script>
    <script src="js/UserDeclaration.js" type="text/javascript"></script>
    <script src="js/PersonalDeclarartion.js" type="text/javascript"></script>
    <script src="js/RelativeDeclaration.js" type="text/javascript"></script>
    <script src="js/DematDeclaration.js" type="text/javascript"></script> 
    <script src="js/InitialHodingDeclaration.js" type="text/javascript"></script>  
    <script type="text/javascript">
        $(function () {
            var webUrl = uri + "/api/PdfViewerIT";
            $("#viewer").ejPdfViewer({
                serviceUrl: webUrl,
                enableStrikethroughAnnotation: false,
                toolbarSettings: { showTooltip: false }
            });
            var pdfViewer = $('#viewer').data('ejPdfViewer');
            pdfViewer.showSelectionTool(false);
            pdfViewer.showPrintTools(false);
            pdfViewer.showDownloadTool(false);
            pdfViewer.showSignatureTool(false);
            pdfViewer.showTextMarkupAnnotationTools(false);
            pdfViewer.showMagnificationTools(false);
            pdfViewer.model.enableTextSelection = false;
        });
    </script>
</asp:Content>
