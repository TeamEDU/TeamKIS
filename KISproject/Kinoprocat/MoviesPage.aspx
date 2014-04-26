<%@ Page Title="Фильмы" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="MoviesPage.aspx.cs" Inherits="KISproject.Kinoprocat.MoviesPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="TimePicker" namespace="MKB.TimePicker" tagprefix="MKB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Разрешает частично воспроизводить разделы страницы без
        обратной передачи (т.е перезагрузки страницы). --%>
    <asp:UpdatePanel ID="MoviesPanel" runat="server">
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
                Text="Изменить" OnClick="btnEdit_Click" />
    
            &nbsp;<asp:Button ID="btnDelete" runat="server"
                CausesValidation="False" Enabled="False"
                Text="Удалить" OnClick="btnDelete_Click"  />
            <br />
            <br />
            
            <%-- Таблица --%>
            <asp:GridView ID="GridViewMovies" runat="server" 
                AutoGenerateColumns="False" AutoGenerateSelectButton="True"
                Caption="Таблица фильмов" EnablePersistedSelection="True"
                SelectedRowStyle-BackColor="Yellow"
                DataKeyNames="id" DataSourceID="SqlDataSourceKISMovie"
                OnSelectedIndexChanged="GridViewMovies_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="Movie_id" ReadOnly="True" 
                        SortExpression="id" HeaderStyle-CssClass="hideGrideColumn"
                        ItemStyle-CssClass="hideGrideColumn"/>
                    <asp:BoundField DataField="Distributors_id"
                        HeaderText="Distributors_id" ReadOnly="True"
                        SortExpression="Distributors_id" 
                        HeaderStyle-CssClass="hideGrideColumn" 
                        ItemStyle-CssClass="hideGrideColumn"/>
                    <asp:BoundField DataField="Title" HeaderText="Название" SortExpression="Title" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="Дата выхода" SortExpression="ReleaseDate" />
                    <asp:BoundField DataField="Genre" HeaderText="Жанр" SortExpression="Genre" />
                    <asp:BoundField DataField="Duration" HeaderText="Длительность" SortExpression="Duration" />
                    <asp:BoundField DataField="Actors" HeaderText="Актеры" SortExpression="Actors" />
                    <asp:BoundField DataField="Age" HeaderText="Возраст" SortExpression="Age" />
                    <asp:BoundField DataField="Country" HeaderText="Страна" SortExpression="Country" />
                    <asp:BoundField DataField="Director" HeaderText="Режиссер" SortExpression="Director" />
                    <asp:BoundField DataField="Name" HeaderText="Дистрибьютор" SortExpression="Name" />
                </Columns>
            </asp:GridView>
            
            <%-- Источник данных --%>
            <asp:SqlDataSource ID="SqlDataSourceKISMovie" runat="server"
                ConnectionString="<%$ ConnectionStrings:kis_cinema_chainConnectionString %>"
                ProviderName="<%$ ConnectionStrings:kis_cinema_chainConnectionString.ProviderName %>"
                SelectCommand="SELECT movies.id, movies.Title, movies.ReleaseDate, movies.Genre, movies.Duration, movies.Actors, movies.Age, movies.Country, movies.Director, movies.Distributors_id, distributors.Name FROM movies INNER JOIN distributors ON movies.Distributors_id = distributors.id">
            </asp:SqlDataSource>
            
            <%-- Таймер --%>
            <asp:Timer ID="ProductTimer" runat="server" Interval="2000" OnTick="ProductTimer_Tick" />
            
            <%-- Панель пользовательского интерфейса для pop-up окна --%>
            <asp:Panel ID="PanelAddEditMovie" runat="server" Width="310px"
                CssClass="modalPopup" Style="display: none" >
                Название:
                <asp:TextBox ID="txtBoxTitle" runat="server" Width="156px"
                    MaxLength="256" style="margin-left: 44px; text-align: left;"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldTitle" runat="server"
                    ControlToValidate="txtBoxTitle" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None"/>
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderTitle" runat="server"
                    TargetControlID="RequiredFieldTitle" HighlightCssClass="validatorCalloutHighlight"/>
                <br />

                Дата выхода:
                <asp:TextBox ID="txtBoxReleaseDate" runat="server" 
                    MaxLength="15" style="margin-left: 18px;" Width="156px"/>

                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="txtBoxReleaseDate" Format="dd.MM.yyyy"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldReleaseDate" runat="server"
                    ControlToValidate="txtBoxReleaseDate" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None"/>
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutFieldReleaseDate"
                    runat="server" TargetControlID="RequiredFieldReleaseDate"
                    HighlightCssClass="validatorCalloutHighlight"/>
                
                <asp:RegularExpressionValidator ID="RegularExpressionReleaseDate" runat="server"
                    ErrorMessage="Неверный формат даты!"
                    ControlToValidate="txtBoxReleaseDate" ForeColor="Red"
                    ValidationExpression="(0[1-9]|[1-2]\d|3[0-1])\.(0[1-9]|1[0-2])\.(\d{4})"
                    Display="None"/>
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExpressionReleaseDate" 
                    runat="server" TargetControlID="RegularExpressionReleaseDate"
                    HighlightCssClass="validatorCalloutHighlight"/>
                <br />
                
                Жанр:
                <asp:TextBox ID="txtBoxGenre" runat="server"
                    MaxLength="256" style="margin-left: 77px;" Width="156px"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldGenre" runat="server"
                    ControlToValidate="txtBoxGenre" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None" />
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderGenre" 
                    runat="server" TargetControlID="RequiredFieldGenre" 
                    HighlightCssClass="validatorCalloutHighlight"/>
                <br />
                
                Длительность:
                <MKB:TimeSelector ID="TimeSelectorDuration" runat="server" 
                    SelectedTimeFormat="TwentyFour" AllowSecondEditing="True"
                    MinuteIncrement="1"/>
                

                Актеры:
                <asp:TextBox ID="txtBoxActors" runat="server" 
                    MaxLength="512" style="margin-left: 63px" Width="156px" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldActors" runat="server"
                    ControlToValidate="txtBoxActors" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None" />
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderActors" runat="server"
                    TargetControlID="RequiredFieldActors" HighlightCssClass="validatorCalloutHighlight" />
                <br/>

                Возраст:
                <asp:TextBox ID="txtBoxAge" runat="server" Width="156px" 
                    MaxLength="5" style="margin-left: 56px"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldAge" runat="server"
                    ControlToValidate="txtBoxAge" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None"/>
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderAge" runat="server"
                    TargetControlID="RequiredFieldAge" HighlightCssClass="validatorCalloutHighlight"/>
                <br />

                Страна:
                <asp:TextBox ID="txtBoxCountry" runat="server" style="margin-left: 61px;" 
                    MaxLength="256" Width="156px"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldCountry" runat="server"
                    ControlToValidate="txtBoxCountry" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None" />
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderCountry" runat="server"
                    TargetControlID="RequiredFieldCountry" HighlightCssClass="validatorCalloutHighlight" />
                <br/>
                
                Режисер:
                <asp:TextBox ID="txtBoxDirector" runat="server" 
                    MaxLength="256" style="margin-left: 48px" Width="156px"/>
                
                <asp:RequiredFieldValidator ID="RequiredFieldDirector" runat="server"
                    ControlToValidate="txtBoxDirector" ErrorMessage="Заполните поле!"
                    ForeColor="Red" Display="None" />
                
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderDirector" 
                    runat="server" TargetControlID="RequiredFieldDirector"
                    HighlightCssClass="validatorCalloutHighlight" />
                <br/>

                Дистрибьютор:
                <asp:DropDownList ID="DDListDisributor" runat="server"
                    style="margin-left: 2px" Width="160px"
                    DataTextField="Name" DataValueField="id" 
                    DataSourceID="SqlDataSourceKISDistributor" />

                <asp:SqlDataSource ID="SqlDataSourceKISDistributor" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:kis_cinema_chainConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:kis_cinema_chainConnectionString.ProviderName %>" 
                    SelectCommand="SELECT id, Name FROM distributors">
                </asp:SqlDataSource>
                
                <asp:Button ID="btnOk" runat="server" Text="Подтвердить" OnClick="btnOk_Click" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Отмена" CausesValidation="False" OnClick="btnCancel_Click" />
            </asp:Panel>
            
            <%-- Идентификатор идентификатор серверного элемента управления,
            который вызывает появление диалогового окна при нажатии.  --%>
            <asp:ModalPopupExtender ID="ModalPopupWindow" runat="server"
                TargetControlID="btnDummy"
                PopupControlID="PanelAddEditMovie"
                BackgroundCssClass="modalBackground"
                DropShadow="True">
            </asp:ModalPopupExtender>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
