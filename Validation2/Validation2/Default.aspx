<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Validation2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>>            
            <h4>Enter divisible</h4>
            <p>                
                <asp:TextBox runat="server" ID="txtNumber"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RegFielValidNumber"
                    runat="server" ErrorMessage="Need enter number"
                    ControlToValidate="txtNumber">*</asp:RequiredFieldValidator>

                <asp:CustomValidator ID="NumberValidator" runat="server"
                    
                    EnableClientScript="true"
                    ControlToValidate="txtNumber"
                    ErrorMessage="Fill in the field"
                    ClientValidationFunction="TextBoxNumberClient"
                    OnServerValidate="TextBoxNumber_Validate" Display="Dynamic">
                </asp:CustomValidator>

                <asp:RangeValidator ID="RangeValidNumber" 
                    runat="server" ErrorMessage="Error number"
                    Type="Integer"
                    ControlToValidate="txtNumber"
                    MinimumValue="-2147483648"
                    MaximumValue="2147483647"
                    Text="Value need in diapazon Int32"></asp:RangeValidator>
                <asp:Label runat="server" ID="lblNumber" ></asp:Label>
            </p>
        </div>
        <div>   
            <h4>Enter divider</h4>
            <p>                
                <asp:TextBox runat="server" ID="txtDiv"></asp:TextBox>

                <asp:RequiredFieldValidator ID="RequiredFieldValidDiv" 
                    runat="server" ErrorMessage="Need enter number"
                    ControlToValidate="txtNumber">*</asp:RequiredFieldValidator>

                <asp:CustomValidator ID="CustomValidDiv" runat="server"                    
                    EnableClientScript="true"
                    ControlToValidate="txtDiv"
                    ErrorMessage="Fill in the field"
                    ClientValidationFunction="TextBoxNumberClient"
                    OnServerValidate="TextBoxNumber_Validate" Display="Dynamic">
                </asp:CustomValidator>

                <asp:RangeValidator ID="RangeValidDiv" 
                    runat="server" ErrorMessage="Error number"
                    Type="Integer"
                    ControlToValidate="txtDiv"
                    MinimumValue="-2147483648"
                    MaximumValue="2147483647"
                    Text="Value need in diapazon Int32"></asp:RangeValidator>

                <asp:CompareValidator ID="CompareValidatorZero"                    
                    runat="server" ErrorMessage="Error number zero"
                    ControlToValidate="txtDiv"
                    Operator="NotEqual"
                    ValueToCompare="0"></asp:CompareValidator>
                <asp:Label runat="server" ID="lblDiv"></asp:Label>
            </p>
        </div>
        <div>   
            <h3>Result: </h3>
            <p>                 
                <asp:Button runat="server" ID="btnResult"
                    Text="Enter"
                    OnClick="btnResult_Click"
                    CausesValidation="true" Width="70px"></asp:Button>
            </p>
            <p>
                <asp:Label ID="lblRes" runat="server"></asp:Label>
            </p>
        </div>
        <div>
            <asp:ValidationSummary ID="ValidationSummary" runat="server"
                DisplayMode="List"
                HeaderText="As a result of the check, the following errors:" />
        </div>
    </div>
</asp:Content>

