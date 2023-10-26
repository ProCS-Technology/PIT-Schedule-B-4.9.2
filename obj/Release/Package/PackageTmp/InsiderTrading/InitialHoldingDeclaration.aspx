<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/InsiderTrading/InsiderTradingMaster.Master" CodeBehind="InitialHoldingDeclaration.aspx.cs" Inherits="ProcsDLL.InsiderTrading.InitialHoldingDeclaration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <%--    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../assets/global/css/components.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../assets/global/css/plugins.min.css" rel="stylesheet" type="text/css" />--%>
    <!-- END THEME GLOBAL STYLES -->
    <link href="../assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../assets/layouts/layout/css/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/layouts/layout/css/themes/darkblue.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../assets/layouts/layout/css/custom.min.css" rel="stylesheet" type="text/css" />

    <style>
        .form-wizard .steps > li > a.step > .desc, .form-wizard .steps > li > a.step > .number {
            display: inline-block !important;
            font-size: 12px !important;
            font-weight: 300 !important;
        }

        .required {
            color: red !important;
        }
    </style>
    <!-- END THEME LAYOUT STYLES -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-content-inner">
        <div class="row">
            <div class="col-md-12">
                <div class="portlet light portlet-fit " id="form_wizard_1">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class=" icon-layers font-red"></i>
                            <span class="caption-subject font-red bold uppercase">Transaction Screen -
                                               
                                <span class="step-title">Step 1 of 5 </span>
                            </span>
                        </div>
                        <div class="actions">
                            <%--<a class="btn btn-circle btn-icon-only btn-default" href="javascript:;">
                                <i class="icon-cloud-upload"></i>
                            </a>
                            <a class="btn btn-circle btn-icon-only btn-default" href="javascript:;">
                                <i class="icon-wrench"></i>
                            </a>
                            <a class="btn btn-circle btn-icon-only btn-default" href="javascript:;">
                                <i class="icon-trash"></i>
                            </a>--%>
                        </div>
                    </div>
                    <div class="portlet-body form">
                        <form class="form-horizontal" action="#" id="submit_form" method="POST">
                            <div class="form-wizard">
                                <div class="form-body">
                                    <ul class="nav nav-pills nav-justified steps">
                                        <li>
                                            <a href="#tab1" data-toggle="tab" class="step">
                                                <span class="number">1 </span>
                                                <span class="desc">Personal Information </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab2" data-toggle="tab" class="step">
                                                <span class="number">2 </span>
                                                <span class="desc">Add Relative Details </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab3" data-toggle="tab" class="step active">
                                                <span class="number">3 </span>
                                                <span class="desc">Add Demat Accounts </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab4" data-toggle="tab" class="step">
                                                <span class="number">4 </span>
                                                <span class="desc">Initial Holdings </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab5" data-toggle="tab" class="step">
                                                <span class="number">5 </span>
                                                <span class="desc">Confirmation </span>
                                            </a>
                                        </li>
                                        <%--          <li>
                                            <a href="#tab6" data-toggle="tab" class="step">
                                                <span class="number">6 </span>
                                                <span class="desc">
                                                    <i class="fa fa-check"></i>Initial Holding Declaration Step 2 Continued </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab7" data-toggle="tab" class="step">
                                                <span class="number">7 </span>
                                                <span class="desc">
                                                    <i class="fa fa-check"></i>Initial Holding Declaration Step 2 Continued - Confirmation on Submit </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab8" data-toggle="tab" class="step">
                                                <span class="number">8 </span>
                                                <span class="desc">
                                                    <i class="fa fa-check"></i>Submit the email Template of Holding Declaration Step 3 </span>
                                            </a>
                                        </li>
                                        <li>
                                            <a href="#tab9" data-toggle="tab" class="step">
                                                <span class="number">9 </span>
                                                <span class="desc">
                                                    <i class="fa fa-check"></i>Initial Holding Declaration Step 2 </span>
                                            </a>
                                        </li>--%>
                                    </ul>
                                    <div id="bar" class="progress progress-striped" role="progressbar">
                                        <div class="progress-bar progress-bar-success"></div>
                                    </div>
                                    <div class="tab-content">
                                        <div class="alert alert-danger display-none">
                                            <button class="close" data-dismiss="alert"></button>
                                            You have some form errors. Please check below.
                                        </div>
                                        <div class="alert alert-success display-none">
                                            <button class="close" data-dismiss="alert"></button>
                                            Your form validation is successful!
                                        </div>
                                        <div class="tab-pane active" id="tab1">
                                            <h5 style="padding-left: 23px">Provide your personal details</h5>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Username
                                                                   
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtUserName" type="text" class="form-control" name="username" readonly="readOnly" />
                                                    <%--<span class="help-block">Provide your username </span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Login ID
                                                                   
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtLoginId" runat="server" type="text" class="form-control" name="login_id" readonly="readOnly" />
                                                    <%--<span class="help-block">Provide your employee id </span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Company
                                                                   
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtCompanyName" runat="server" type="text" class="form-control" name="company_name" readonly="readOnly" />
                                                    <%--<span class="help-block">Provide your company name </span>--%>
                                                </div>
                                            </div>
                                            <%--      <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Password
                                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input type="password" class="form-control" name="password" id="submit_form_password" />
                                                    <span class="help-block">Provide your password. </span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Confirm Password
                                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input type="password" class="form-control" name="rpassword" />
                                                    <span class="help-block">Confirm your password </span>
                                                </div>
                                            </div>--%>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Email
                                                                   
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtEmailId" type="text" class="form-control" name="email" />
                                                    <%--<span class="help-block">Provide your email address </span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Role
                                                                   
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtRole" type="text" class="form-control" name="role" readonly="readOnly" />
                                                    <%--<span class="help-block">Provide your email address </span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Employee ID<span class="required">*</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtEmployeeId" type="text" class="form-control" name="employee_id" />
                                                    <%--<span class="help-block">Provide your employee id </span>--%>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    SSN #<span class="required">*</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtSsn" type="text" class="form-control" name="ssn" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Resident Type
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="resident_type" id="ddlResidentType" class="form-control">
                                                        <option value=""></option>
                                                        <option value="NRI">NRI</option>
                                                        <option value="FOREIGN_NATIONAL">FOREIGN NATIONAL</option>
                                                        <option value="INDIAN_RESIDENT">INDIAN RESIDENT</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Country
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <select disabled="disabled" name="country" id="country" class="form-control">
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
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Permanent Account Number--%>
                                                    PAN
                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtPermanentAccountNumber" type="text" class="form-control" name="permanent_account_number" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Identification Type
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="identification_type" id="ddlIdentificationType" class="form-control">
                                                        <option value=""></option>
                                                        <option value="DRIVING_LICENSE">DRIVING LICENSE</option>
                                                        <option value="AADHAR_CARD">AADHAR CARD</option>
                                                        <option value="PASSPORT">PASSPORT</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Identification Number--%>
                                                     Identification #
                                                    <%--<span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtIdentificationNumber" type="text" class="form-control" name="identification_number" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Mobile Number--%>
                                                    Mobile #
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtMobileNumber" type="number" class="form-control" name="mobile_number" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Address
                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtAddress" type="text" class="form-control" name="address" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Pincode--%>
                                                    PIN
                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtPincodeNumber" type="number" class="form-control" name="pincode_number" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Date Of Joining--%>
                                                    DOJ
                                                   
                                                    <span class="required">* </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtDateOfJoining" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="date_Of_Joining" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%--Date Of Becoming Insider--%>
                                                    Date Becoming Insider
                                                   
                                                   <%-- <span class="required">* </span>--%>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtDateOfBecomingInsider" data-date-format="dd/mm/yyyy" type="text" class="form-control date-picker" name="date_Of_Becoming_Insider" />
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Corporate Location
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="location" id="ddlLocation" class="form-control">
                                                        <option value=""></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="form-group" style="display:none;">
                                                <label class="control-label col-md-3">
                                                    Category
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="category" id="ddlCategory" class="form-control">
                                                        <option value=""></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divSubCategory" style="display:none;">
                                                <label class="control-label col-md-3">
                                                    Sub Category
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="sub_category" id="ddlSubCategory" class="form-control">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Department
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="department" id="ddlDepartment" class="form-control">
                                                        <option value=""></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    Designation
                                                </label>
                                                <div class="col-md-4">
                                                    <select name="designation" id="ddlDesignation" class="form-control" onchange="javascript:ddlDesignation_onChange();">
                                                        <option value=""></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <%-- DIN Number--%>
                                                    DIN #
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtDinNumber" type="text" class="form-control" name="din_number" />
                                                    <input id="txtD_ID" type="hidden" value="0" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label id="lblInstitution" class="control-label col-md-3">
                                                    Name of the Educational Institution of Graduation<span class="required"> * </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtInstitution" type="text" class="form-control" name="name_of_educational_institution_of_graduation" autocomplete="off" />
                                                </div>
                                            </div>
                                            <div class="form-group" style="display:none;">
                                                <label id="lblStream" class="control-label col-md-3">
                                                    Stream of Graduation<span class="required"> * </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtStream" type="text" class="form-control" name="stream_of_graduation" autocomplete="off" />
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label id="lblEmployer" class="control-label col-md-3">
                                                    Details of the Past Employers (if applicable)
                                                </label>
                                                <div class="col-md-4">
                                                    <input id="txtEmployer" type="text" class="form-control" name="details_of_past_employer" autocomplete="off" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab2">
                                            <h5 style="padding-left: 23px">Provide your relative details (<span style="color: red;">Please Add Material Financial Relative Information if you have provided more than 25% of Your Annual Income</span>)</h5>
                                            <div style="margin-left: 20px" class="actions">
                                                <a class="btn btn-default" data-toggle="modal" data-target="#modalAddRelativeDetail">
                                                    <i class="fa fa-plus"></i>Add
                                                </a>
                                                <br />
                                                <br />
                                                <table class="table table-striped table-hover table-bordered" id="tbl-Relative-setup" style="width: 1000px">
                                                    <thead>
                                                        <tr>
                                                            <th style="display: none">RELATIVE ID</th>
                                                            <%--<th>RELATIVE NAME</th>--%>
                                                            <th>RELATIVE</th>
                                                            <th style="display: none">RELATION ID</th>
                                                            <th>RELATION</th>
                                                            <th>PAN</th>
                                                            <th>IDENTIFICATION</th>
                                                            <%--<th>IDENTIFICATION NUMBER</th>--%>
                                                            <th>ID #</th>
                                                            <th>ADDRESS</th>
                                                            <th>PHONE #</th>
                                                            <th>STATUS</th>
                                                            <th>REMARKS</th>
                                                            <th>ACTION</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdRelativeList">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab3">
                                            <h5 style="padding-left: 23px">Provide your DEMAT Account Details (ALL DEMAT ACCOUNTS REQUIRED)</h5>
                                            <div style="margin-left: 20px" class="actions">
                                                <a class="btn btn-default" data-toggle="modal" data-target="#modalAddDematDetail">
                                                    <i class="fa fa-plus"></i>Add
                                                </a>
                                                <br />
                                                <br />
                                                <table class="table table-striped table-hover table-bordered" id="tbl-Demat-setup" style="width: 1000px">
                                                    <thead>
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
                                                            <th>TRADING MEMBER ID</th>
                                                            <%--<th>DEMAT ACCOUNT NO</th>--%>
                                                            <th>DEMAT</th>
                                                            <th>STATUS</th>
                                                            <th>ACTION</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdDematList">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab4">
                                            <h5 style="padding-left: 23px">Add Initial Holding Declaration</h5>
                                            <div style="margin-left: 20px" class="actions">
                                                <a class="btn btn-default" data-toggle="modal" data-target="#modalAddInitialHoldingDeclarations">
                                                    <i class="fa fa-plus"></i>Add
                                                </a>
                                                <br />
                                                <br />
                                                <table class="table table-striped table-hover table-bordered" id="tbl-InitialHolding-setup" style="width: 1000px">
                                                    <thead>
                                                        <tr>
                                                            <th style="display: none;">RESTRICTED COMPANY ID</th>
                                                            <th>COMPANY</th>
                                                            <th style="display: none;">SECURITY TYPE ID</th>
                                                            <th>SECURITY TYPE</th>
                                                            <th style="display: none;">RELATIVE ID</th>
                                                            <th>FOR</th>
                                                            <th style="display: none;">DEMAT ACCOUNT ID</th>
                                                            <th>DEMAT ACCOUNT NO</th>
                                                            <th>NUMBER OF SECURITIES</th>
                                                            <th style="display: none;">PAN</th>
                                                            <th style="display: none;">TRADING MEMBER ID</th>
                                                            <th>ACTION</th>
                                                            <th style="display: none;">INITIAL HOLDING ID</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdInitialDeclaration">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="tab-pane" id="tab5">
                                            <h5 style="padding-left: 23px">Send Initial Holding</h5>
                                            <table class="table table-striped table-hover table-bordered" style="margin-left: 23px; width: 1000px;">
                                                <%--                                              <thead>
                                                    <tr>
                                                        <th></th>
                                                        <th></th>
                                                    </tr>
                                                </thead>--%>
                                                <tbody>
                                                    <tr>
                                                        <td>Mail To</td>
                                                        <td id="tdMailToUserName"></td>
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
                                                        <td id="tdSubject">Declarations Of Insider for the Financial Year 20-21</td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <label style="padding-left: 23px;">Body</label>
                                            <table class="table table-striped table-hover table-bordered" style="margin-left: 23px; width: 1000px;">
                                                <thead>
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
                                                            <div id="viewer" style="height: 700px; width: 950px; min-height: 404px;"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%--<input type="checkbox" id="inAcceptTermsAndConditions" />&nbsp;<span>I hereby declare that I accept the terms and conditions of this policy.</span>&nbsp;<a href="#" id="aInAcceptTermsAndConditions" target="_blank">Please click here to read the Insider Trading Policy</a></td>--%>
                                                            <input type="checkbox" id="inAcceptTermsAndConditions" />&nbsp;<span>I hereby declare that I accept the terms and conditions of this policy.</span></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <%--     <div class="control">
                                                <ej:PdfViewer ID="PdfViewer1" Height="800"
                                                    ServiceUrl="api/PdfViewerIT"
                                                    PdfService="Local"
                                                    runat="server">
                                                </ej:PdfViewer>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <div class="row">
                                        <div class="col-md-offset-0 col-md-9" style="margin-left: 20px;">
                                            <a href="javascript:;" class="btn default button-previous">
                                                <i class="fa fa-angle-left"></i>

                                                Back 
                                            </a>
                                            <a href="javascript:fnSaveScreenInformation();" class="btn btn-outline green button-next">Save & Continue
                                                               
                                                <i class="fa fa-angle-right"></i>
                                            </a>
                                            <a id="aSubmitYourDeclaration" style="pointer-events: none; color: #666; background-color: #e1e5ec; border-color: #e1e5ec;" disabled="true" href="javascript:fnSendEmailNoticeConfirmation();" class="btn green button-submit">Submit Your Declaration 
                                                <i class="fa fa-check"></i>
                                            </a>
                                            <%--         <a href="javascript:void(0);" class="btn green button-submit">Save As Draft
                                                               
                                                <i class="fa fa-check"></i>
                                            </a>--%>
                                            <%--        <a href="javascript:fnConfirmationPrint();" class="btn green button-submit">Print
                                                               
                                                <i class="fa fa-check"></i>
                                            </a>--%>
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

    <div class="modal fade" id="modalAddRelativeDetail" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 700px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalAddRelativeDetail();"></button>
                    <h4 class="modal-title">Relative Detail</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divRelativeDetail" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblRelation" style="text-align: left" class="col-md-4 control-label">Relation<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlRelation" class="form-control" onchange="javascript:fnRemoveClass(this,'Relation');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblName" style="text-align: left" class="col-md-4 control-label">Name<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtName" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'Name');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblSkipPan" class="col-md-4 control-label">Please check here in case PAN number of Relative is not available</label>
                                    <div class="col-md-8">
                                        <input id="txtSkipPan" type="checkbox" onclick="javascript: fnCheckPanMandatory();" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblPAN" style="text-align: left" class="col-md-4 control-label">PAN<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtPAN" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'PAN');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblIdentificationTypeRelation" style="text-align: left" class="control-label col-md-4">
                                        Identification Type
                                    </label>
                                    <div class="col-md-8">
                                        <select name="identification_type_relation" id="ddlIdentificationTypeRelation" class="form-control" onchange="javascript:fnRemoveClass(this,'IdentificationTypeRelation');">
                                            <option value=""></option>
                                            <option value="NotApplicable">Not Applicable</option>
                                            <option value="DRIVING_LICENSE">DRIVING LICENSE</option>
                                            <option value="AADHAR_CARD">AADHAR CARD</option>
                                            <option value="PASSPORT">PASSPORT</option>
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblIdentificationNumberRelation" class="control-label col-md-4">
                                        Identification Number
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtIdentificationNumberRelation" type="text" class="form-control" name="identification_number_relation" onkeypress="javascript:fnRemoveClass(this,'IdentificationNumberRelation');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblSameAddressRelation" class="col-md-4 control-label">Same Address As Yours</label>
                                    <div class="col-md-8">
                                        <input id="txtSameAddressRelation" type="checkbox" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div class="form-group">
                                    <label id="lblAddressRelation" class="col-md-4 control-label">Address<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtAddressRelation" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'AddressRelation');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div class="form-group">
                                    <label id="lblPhoneRelation" class="col-md-4 control-label">Phone</label>
                                    <div class="col-md-8">
                                        <input id="txtPhoneRelation" type="number" max="15" class="form-control" onkeypress="javascript:fnRemoveClass(this,'PhoneRelation');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div class="form-group">
                                    <label id="lblStatusRelation" class="control-label col-md-4">
                                        Status
                                                    <span class="required">* </span>
                                    </label>
                                    <div class="col-md-8">
                                        <select name="Status_Relation" id="ddlStatusRelation" class="form-control" onchange="javascript:fnRemoveClass(this,'StatusRelation');">
                                            <option value=""></option>
                                            <option value="NotApplicable">Not Applicable</option>
                                            <option value="ACTIVE">ACTIVE</option>
                                            <option value="INACTIVE">INACTIVE</option>
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div class="form-group">
                                    <label id="lblRelativeRemarks" class="col-md-3 control-label">
                                        <span id="spRelativeRemarks">Remarks</span>
                                        <span id="spMaterialFinancialRelationship" style="display: none;">Reasons for considering the person as a person with whom ‘Material Financial Relationship’ is shared
                                        <span class="required">* </span>
                                        </span>
                                    </label>
                                    <div class="col-md-8" style="margin-left: 53px;">
                                        <textarea id="remarks" class="form-control" onkeypress="javascript:fnRemoveClass(this,'RelativeRemarks');"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalAddRelativeDetail();">Close</button>
                    <button id="btnSaveRelativeDetail" type="button" class="btn green" onclick="javascript:fnAddRelativeDetail();">Add</button>
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
                                            <option value="Physical_Shares">Physical Shares</option>
                                            <option value="Others">Others</option>
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblDepositoryParticipantName" style="text-align: left" class="control-label col-md-4">
                                        Depository Participant Name
                                                    <%--<span class="required">* </span>--%>
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
                                            <input id="txtDepositoryParticipantId" type="number" max="8" class="form-control" onkeypress="javascript:fnRemoveClass(this,'DepositoryParticipantId');" autocomplete="off" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblClientId" style="text-align: left" class="col-md-4 control-label">Client Id<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtClientId" type="number" max="8" class="form-control" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="8" onkeypress="javascript:fnRemoveClass(this,'ClientId');" autocomplete="off" />
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
                                <div class="form-group">
                                    <label id="lblTradingMemberId" style="text-align: left" class="col-md-4 control-label">Trading MemberId Id<%--<span class="required"> * </span>--%></label>
                                    <div class="col-md-8">
                                        <input id="txtTradingMemberId" type="text" class="form-control" oninput="javascript: if (this.value.length > this.maxLength) this.value = this.value.slice(0, this.maxLength);" maxlength="254" onkeypress="javascript:fnRemoveClass(this,'TradingMemberId');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblStatusDemat" class="control-label col-md-4">
                                        Status
                                                    <span class="required">* </span>
                                    </label>
                                    <div class="col-md-8">
                                        <select name="Status_Demat" id="ddlStatusDemat" class="form-control" onchange="javascript:fnRemoveClass(this,'StatusDemat');">
                                            <option value=""></option>
                                            <option value="NotApplicable">Not Applicable</option>
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
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalAddDematDetail();">Close</button>
                    <button id="btnSaveDematDetail" type="button" class="btn green" onclick="javascript:fnAddDematDetail();">Add</button>
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
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divInitialHoldingDeclaration" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblRestrictedCompanies" style="text-align: left" class="col-md-4 control-label">Company<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlRestrictedCompanies" class="form-control" onchange="javascript:fnRemoveClass(this,'RestrictedCompanies');">
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblSecurityType" style="text-align: left" class="col-md-4 control-label">Security Type<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <select id="ddlSecurityType" class="form-control" onchange="javascript:fnRemoveClass(this,'SecurityType');">
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
                                <div class="form-group">
                                    <label id="lblTradingMemId" style="text-align: left" class="col-md-4 control-label">Trading MemberId Id</label>
                                    <div class="col-md-8">
                                        <input id="txtTradingMemId" type="text" class="form-control" readonly="readOnly" onkeypress="javascript:fnRemoveClass(this,'TradingMemId');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblNumberOfSecurities" style="text-align: left" class="col-md-4 control-label">Number Of Securities<span class="required"> * </span></label>
                                    <div class="col-md-8">
                                        <input id="txtNumberOfSecurities" type="number" max="10" class="form-control" onkeypress="javascript:fnRemoveClass(this,'NumberOfSecurities');" autocomplete="off" />
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
                    <input value="YES" id="btnDeleteInitialHoldingDetail" data-dismiss="modal" class="btn red" onclick="fnDeleteInitialHoldingDetailModal()" type="submit" />
                </div>
            </div>
        </div>
    </div>

    <%--    <div class="modal fade" id="modalAddEducationalAndProfessionalDetails" tabindex="-1" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" style="width: 70%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true" onclick="javascript:fnCloseModalEducationAndProfessionalDetails();"></button>
                    <h4 class="modal-title">Please provide the following details for  Annual Disclosure by Designated Persons</h4>
                </div>
                <div class="modal-body" style="overflow-y: scroll;">
                    <div class="portlet-body form">
                        <div class="row">
                            <div id="divEduAndProf" class="col-md-12">
                                <div class="form-group">
                                    <label id="lblInstitution" style="text-align: left" class="control-label col-md-4">
                                        Name of the Educational Institution of Graduation<span class="required"> * </span>
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtInstitution" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'Institution');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblStream" style="text-align: left" class="control-label col-md-4">
                                        Stream of Graduation<span class="required"> * </span>
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtStream" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'Stream');" autocomplete="off" />
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="form-group">
                                    <label id="lblEmployer" style="text-align: left" class="control-label col-md-4">
                                        Details of the Past Employers (if applicable)
                                    </label>
                                    <div class="col-md-8">
                                        <input id="txtEmployer" type="text" class="form-control" onkeypress="javascript:fnRemoveClass(this,'Employer');" autocomplete="off" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" data-dismiss="modal" class="btn default" onclick="javascript:fnCloseModalEducationAndProfessionalDetails();">Close</button>
                    <button id="btnSaveEducationalAndProfessionalDetails" type="button" class="btn green" onclick="javascript:fnSubmitEducationalAndProfessionalDetails();">Add</button>
                </div>
            </div>
        </div>
    </div>--%>

    <!-- BEGIN CORE PLUGINS -->
    <%--<script src="../assets/global/plugins/jquery.min.js" type="text/javascript"></script>--%>
    <%--<script src="../assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <script src="../assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <script src="../assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
    <script src="../assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="../assets/pages/scripts/components-date-time-pickers.min.js" type="text/javascript"></script>
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <%--   <script src="../assets/global/scripts/app.min.js" type="text/javascript"></script>--%>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN PAGE LEVEL SCRIPTS -->
    <%--<script src="../assets/pages/scripts/form-wizard.min.js" type="text/javascript"></script>--%>
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <%-- <script src="../assets/layouts/layout/scripts/layout.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/layout/scripts/demo.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>--%>
    <!-- END THEME LAYOUT SCRIPTS -->
    <%--    <script>
        $(document).ready(function () {
            $('#clickmewow').click(function () {
                $('#radio1003').attr('checked', 'checked');
            });
        })
        </script>--%>
    <script src="js/Global.js?<%=DateTime.Now %>"></script>
    <script type="text/javascript">
        $(function () {
            var webUrl = uri + "/api/PdfViewerIT";
            $("#viewer").ejPdfViewer({
                serviceUrl: webUrl,
                //documentPath: "HTTP Succinctly",
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
    <script src="js/Transaction.js?<%=DateTime.Now %>" type="text/javascript"></script>

</asp:Content>
