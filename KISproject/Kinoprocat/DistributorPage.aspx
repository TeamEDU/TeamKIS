<%@ Page Title="Дистрибьюторы" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DistributorPage.aspx.cs" Inherits="KISproject.Kinoprocat.DistributorPage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%-- Контент для заголовка --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<%-- Основной контент --%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Разрешает частично воспроизводить разделы страницы без
         обратной передачи (т.е перезагрузки страницы). --%>
    <asp:UpdatePanel ID="DistribPanel" runat="server">
        <ContentTemplate>
            <%-- Кнопка заглушка для ModalPopupExtender.TargetControlID.
                Необходима, т.к иначе у btnAdd не сработает обраб. события --%>
            <asp:Button ID="btnDummy" runat="server" Style="display: none" />

            <%-- Контент для заголовка --%>
            <asp:Button ID="btnAdd" runat="server"
                CausesValidation="False"
                Text="Добавить" OnClick="btnAdd_Click" />
    
            &nbsp;<asp:Button ID="btnEdit" runat="server"
                CausesValidation="False" Enabled="False"
                Text="Изменить" OnClick="btnEdit_Click"/>
    
            &nbsp;<asp:Button ID="btnDelete" runat="server"
                CausesValidation="False" Enabled="False"
                Text="Удалить" OnClick="btnDelete_Click" />               
            <br />
            <br />
            
            <%-- Таблица --%>
            <asp:GridView ID="GridViewDistributors" runat="server"
                DataSourceID="SqlDataSourceKISDistributor" AutoGenerateColumns="False"
                AutoGenerateSelectButton="True"
                Caption="Таблица дистрибьюторов"
                OnSelectedIndexChanged="GridViewDistribution_SelectedIndexChanged"
                DataKeyNames="id" 
                EnablePersistedSelection="True" SelectedRowStyle-BackColor="Yellow">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Distributor_id"
                        SortExpression="id" ReadOnly="True"
                        HeaderStyle-CssClass="hideGrideColumn" ItemStyle-CssClass="hideGrideColumn"/>
                    <asp:BoundField DataField="Contacts_id" HeaderText="Contacts_id"
                        SortExpression="Contacts_id" ReadOnly="True"
                        HeaderStyle-CssClass="hideGrideColumn" ItemStyle-CssClass="hideGrideColumn"/>
                    <asp:BoundField DataField="Name" HeaderText="Имя" SortExpression="Name" />
                    <asp:BoundField DataField="Phone" HeaderText="Телефон" SortExpression="Phone" />
                    <asp:BoundField DataField="Email" HeaderText="Почта" SortExpression="Email" />
                    <asp:BoundField DataField="Address" HeaderText="Адрес" SortExpression="Address" />
                </Columns>
            </asp:GridView>
            
            <%-- Источник данных --%>
            <asp:SqlDataSource ID="SqlDataSourceKISDistributor" runat="server"
                ConnectionString="<%$ ConnectionStrings:kis_cinema_chainConnectionString %>"
                ProviderName="<%$ ConnectionStrings:kis_cinema_chainConnectionString.ProviderName %>"
                SelectCommand="SELECT distributors.*, contacts.Phone, contacts.Email, contacts.Address FROM contacts INNER JOIN distributors ON contacts.id = distributors.Contacts_id">
            </asp:SqlDataSource>
            
            <%-- Таймер --%>
            <asp:Timer ID="ProductTimer" runat="server" Interval="2000" OnTick="ProductTimer_Tick"/>
            
            <%-- Панель пользовательского интерфейса для pop-up окна --%>
            <asp:Panel ID="PanelAddEditDistributor" runat="server"  Width="250px"
                CssClass="modalPopup" Style="display: none">
                Имя:
                <asp:TextBox ID="txtBoxName" runat="server" style="margin-left: 48px"
                    MaxLength="256"/>

                <asp:RequiredFieldValidator ID="RequiredFieldName" runat="server" 
                    ControlToValidate="txtBoxName" ErrorMessage="Заполните поле!" 
                    ForeColor="Red" Display="None"/>

                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderName" runat="server"
                    TargetControlID="RequiredFieldName" HighlightCssClass="validatorCalloutHighlight"/>
                <br />

                Телефон:
                <asp:TextBox ID="txtBoxPhone" runat="server" style="margin-left: 8px"
                    MaxLength="256" />
        
                <asp:RequiredFieldValidator ID="RequiredFieldPhone" runat="server" 
                    ControlToValidate="txtBoxPhone" ErrorMessage="Заполните поле!" ForeColor="Red" 
                    Display="None"/>

                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderPhone" runat="server"
                    TargetControlID="RequiredFieldPhone" HighlightCssClass="validatorCalloutHighlight"/>
                <br />
        
                Почта:
                <asp:TextBox ID="txtBoxEmail" runat="server" style="margin-left: 32px"
                    MaxLength="256"/>
        
                <asp:RequiredFieldValidator ID="RequiredFieldEMail" runat="server" 
                    ErrorMessage="Заполните поле!" ControlToValidate="txtBoxEmail" ForeColor="Red"
                    Display="None"/>
                <br />
        
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderEmail" runat="server"
                    TargetControlID="RequiredFieldEMail" HighlightCssClass="validatorCalloutHighlight"/>

                <asp:RegularExpressionValidator ID="RegularExpressionEmail" runat="server"
                    ErrorMessage="Неверно указанный адрес!" 
                    ControlToValidate="txtBoxEmail" ForeColor="Red"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="None"/>

                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderEEmail" runat="server"
                    TargetControlID="RegularExpressionEmail"
                    HighlightCssClass="validatorCalloutHighlight"/>
                <br />

                Адрес:
                <asp:TextBox ID="txtBoxAddress" runat="server" style="margin-left: 30px"
                    MaxLength="256"/>

                <asp:RequiredFieldValidator ID="RequiredFieldAddress" runat="server"
                    ControlToValidate="txtBoxAddress" ErrorMessage="Заполните поле!" ForeColor="Red"
                    Display="None"/>
        
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderAddress" runat="server"
                    TargetControlID="RequiredFieldAddress" HighlightCssClass="validatorCalloutHighlight"/>
                <br />
                <br />

                <asp:Button ID="btnOk" runat="server"
                    Text="Подтвердить" OnClick="btnOk_Click"/>
                &nbsp;
                <asp:Button ID="btnCancel" runat="server"
                    style="margin-left: 13px" CausesValidation="False"
                    Text="Отмена" OnClick="btnCancel_Click"/>
            </asp:Panel>

            <%-- Идентификатор идентификатор серверного элемента управления,
                 который вызывает появление диалогового окна при нажатии.  --%>
            <asp:ModalPopupExtender ID="ModalPopupWindow" runat="server"
                TargetControlID="btnDummy"
                PopupControlID="PanelAddEditDistributor"
                BackgroundCssClass="modalBackground"
                DropShadow="True">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
