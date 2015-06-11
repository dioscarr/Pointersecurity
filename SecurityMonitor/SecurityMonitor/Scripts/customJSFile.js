$(function () {


    var repairflag = "off";
    var allrouteElem = document.querySelectorAll('.route');

    var lengthofElem = allrouteElem.length;
   
    for (var i = 0; i < lengthofElem; i++)
    {
        var elem = allrouteElem[i].dataset.route;
        switch (elem)
        {
            case "Repair":
                if (repairflag == "off")
                {
                    repairflag = "on";
                   
                    var $BID = $('#buildingidforcustomjs').attr("data-buildingid");
                    //ajax call to retreive count from repair 
                    $.ajax({
                        type: "GET",
                        data: { BuildingID: $BID },
                        dataType: "json",
                        url: "/building/LoadOpenRepair/",
                        success: function (jsonData) {
                          
                            var jsonresult = JSON.stringify(jsonData.Data);// Json.stringify make an object into a json string
                            var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName
                           
                            $('[data-open-Count="Repair"]').html(jsonresult);
                        }
                    });
                }
                break;
        }
    }


    $('.route').click(function () {
        var route = $(this).attr("data-route");
        var BID = $('#buildingidforcustomjs').attr("data-buildingid");
        switch (route) {
            case "Repair":
                window.location.href = "/building/Repairmanagement?BuildingID=" + BID;
                break;
        }
    });


    $('body').on('click', function (e) {
        $('[data-toggle="popover"]').each(function () {
            //the 'is' for buttons that trigger popups
            //the 'has' for icons within a button that triggers a popup
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                $(this).popover('hide');
            }
        });
    });

    $('.btn').on('click', function (e) {
        if (typeof $(e.target).data('original-title') == 'undefined' &&
           !$(e.target).parents().is('.popover.in')) {
            $('[data-original-title]').popover('hide');
        }
    });

    $('html').on('click', function (e) {

        $('#popover_content_wrapper').popover('hide');

        if (typeof $(e.target).data('original-title') == 'undefined' &&
           !$(e.target).parents().is('.popover.in')) {
            $('[data-original-title]').popover('hide');
        }
    });

    function tryme() {
        alert("this works");

    };

   
          //model binded to the page  
    function ViewModel() {
        var self = this;
        var buildingID = $('#buildingidforcustomjs').attr("data-buildingid");
        var ClientID = $('#buildingclientid').attr("data-clientid");
       
        self.BuildingName = ko.observable("");
        self.BuildingAddress = ko.observable("");
        self.BuildingCity = ko.observable("");
        self.BuildingState = ko.observable("");
        self.BuildingZipcode = ko.observable("");
        self.BuildingPhone = ko.observable("");
        self.StateList = ko.observableArray("");
        self.miniMenuText = ko.observable("");

        self.ModuleRoute =function (data) {
            var myvalue = ko.toJSON(value);

            var myjson = JSON.parse(myvalue);
            
        };

        //loading Building staff when needed
        //hold permissions
        self.BuildingUsers = ko.observableArray();
        //function call on click
        self.LoadSP = function ()
        {



        };



        //"""""""""""""""""""""""""""""""""Nav to apartment """"""""""""""""""""""""""""""""""""""""""""""""

        self.takemetoApt = function (value, event) {
            var myvalue = ko.toJSON(value);

            var myjson = JSON.parse(myvalue);
           // alert(myjson.AptID);
           // debugger;

            window.location.href = "/building/ApartmentProfile?ApartmentID=" + myjson.AptID + "&BuildingID=" + buildingID;

        };


        //"""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""


        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^load building staff and permissions^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        setTimeout(function ()
        {
            $.ajax({
                type: "GET",
                data: { BuildingID: buildingID },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "/building/LoadStaffPermissions/",
                success: function (jsonData) {
                    var jsonresult = JSON.stringify(jsonData.Data);// Json.stringify make an object into a json string                        
                    var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName                        
                    for (var i = 0; i < TrkPkgs.length; i++) {
                        //permissions
                        var permissiondata = JSON.stringify(TrkPkgs[i].PermisionNames);
                        var TrkPkgsP = JSON.parse(permissiondata);
                        self.P = ko.observableArray();
                        for (var j = 0; j < TrkPkgsP.length; j++) {
                            //alert(TrkPkgsP[j]);
                            self.P.push({ P: TrkPkgsP[j] });
                        }
                        //alert(self.P());
                        self.BuildingUsers.push(
                            {
                                FullName: TrkPkgs[i].FullName,
                                Email: TrkPkgs[i].Email,
                                Phone: TrkPkgs[i].Phone,
                                UserID: TrkPkgs[i].UserID,
                                BSP: self.P()
                            });
                    }
                    //alert(JSON.stringify(self.BuildingUsers()));
                }
            });
        },2000);
        

//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^load building staff and permissions^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^




        self.BuildingFullAddress = ko.computed(function () {
            return self.BuildingAddress() +
                " " + self.BuildingCity() +
                " " + self.BuildingState() +
                " " + self.BuildingZipcode();
        });

        var buildingInfoEdit = {
            BuildingName: self.BuildingName,
            Address: self.BuildingAddress,
            City: self.BuildingCity,
            State: self.BuildingState,
            Zipcode: self.BuildingZipcode,
            ClientID: ClientID,
            ID: buildingID,
            BuildingPhone: self.BuildingPhone
        };


       
        

            $.ajaxSettings.traditional = true;
            $.ajax(
                {
                    type: "GET",
                    url: '/building/BuildingEdit/',
                    dataType: "json",
                    data: { id: buildingID },
                    success: function (data) {
                       
                        var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string
                                               var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName
                        
                        //alert("This is the building's name: "+TrkPkgs.BuildingName);

                        for (var i = 0; i < TrkPkgs.length; i++) {
                            self.BuildingName(TrkPkgs[i].BuildingName);
                            self.BuildingAddress(TrkPkgs[i].BuildingAddress);
                            self.BuildingCity(TrkPkgs[i].BuildingCity);
                            self.BuildingState(TrkPkgs[i].BuildingState);
                            self.BuildingZipcode(TrkPkgs[i].BuildingZipcode);
                            self.BuildingPhone(TrkPkgs[i].BuildingPhone);
                        }
                    }
                });


        self.EditBuildingInfo = function () {
           
            $.ajaxSettings.traditional = true;
            $.ajax(
                {
                    type: "Post",
                    url: '/building/BuildingEdit/',
                    dataType: "json",
                    data: buildingInfoEdit,
                    success: function (data) {

                        
                        var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string

                        var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName
                        for (var i = 0; i < TrkPkgs.length; i++) {
                            self.BuildingName(TrkPkgs[i].BuildingName);
                            self.BuildingAddress(TrkPkgs[i].BuildingAddress);
                            self.BuildingCity(TrkPkgs[i].BuildingCity);
                            self.BuildingState(TrkPkgs[i].BuildingState);
                            self.BuildingZipcode(TrkPkgs[i].BuildingZipcode);
                            self.BuildingPhone(TrkPkgs[i].BuildingPhone);
                        }
                    }
                });

        };
        


        

      
        
        self.AparmentNumber = ko.observableArray();

        self.AptSearch = ko.observable();

        self.AptSearch.subscribe(function(value){

            $.ajax({
                type: "GET",
                url: "/Building/LoadApartmentsSearch/",
                data: { Search: value, BuildingID: buildingID },
                dataType: "json",
                success: function (data)
                {
                    self.AparmentNumber.removeAll();

                    var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string

                    var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName

                    for (var i = 0; i < TrkPkgs.length; i++)
                    {
                        self.AparmentNumber.push({
                            AptID: TrkPkgs[i].AptID,
                            Apt:TrkPkgs[i].Apt
                        });
                    }

                  
                }
            });
        });

        $.ajax({
            type: "GET",
            url: "/Building/LoadApartments/",
            data: { BuildingID: buildingID },
            dataType: "json",
            success: function (data)
            {
                self.AparmentNumber.removeAll();

                var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string

                var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName

                for (var i = 0; i < TrkPkgs.length; i++)
                {
                    self.AparmentNumber.push({
                        AptID: TrkPkgs[i].AptID,
                        Apt:TrkPkgs[i].Apt
                    });
                }
            }
        });


        

        $.ajaxSettings.traditional = true;
        $.ajax(
            {
                type: "GET",
                url: '/building/States/',
                dataType: "json",
                success: function (data) {

                    var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string
                    var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName

                    self.StateList(TrkPkgs);



                }
            });






               Name = ko.observable("");
               //Manager = ko.observable("");
               Address = ko.observable("");
               City = ko.observable("");
               //AptNum = ko.observable();
               State = ko.observable("");
               //State = ko.observable("");
               Phone = ko.observable("")           
               Fax = ko.observable("");
               Zipcode = ko.observable("");
               Email = ko.observable("");
               //this.chrAptNum = ko.computed(function () {if (AptNum() >= 0 )
               //{
               //    $('#idAptNum').css("color", "green");
               //    $('#idAptNum').removeClass('glyphicon glyphicon-asterisk');
               //    $('#idAptNum').addClass('glyphicon glyphicon-ok');
                  
               //} else {
               //    $('#idAptNum').removeClass('glyphicon glyphicon-ok');
               //    $('#idAptNum').css("color", "red");
               //    $('#idAptNum').addClass('glyphicon glyphicon-asterisk');
               //}});
               this.chrName = ko.computed(function ()
               { 
                   if (self.BuildingName.length > 0  ) {
                       $('#idName').css("color", "green");
                       $('#idName').removeClass('glyphicon glyphicon-asterisk');
                       $('#idName').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingName.length;
                   } else {
                       $('#idName').removeClass('glyphicon glyphicon-ok');
                       $('#idName').css("color", "red");
                       $('#idName').addClass('glyphicon glyphicon-asterisk');
                   } 
               });

               //this.chrManager = ko.computed(function ()
               //{
               //    if (Manager().length > 0)
               //    {$('#idmanager').css("color", "green");
               //        $('#idmanager').removeClass('glyphicon glyphicon-asterisk');
               //        $('#idmanager').addClass('glyphicon glyphicon-ok');
               //        return 'chars: ' + Manager().length;
               //    } else {
               //        $('#idmanager').removeClass('glyphicon glyphicon-ok');
               //        $('#idmanager').css("color", "red");
               //        $('#idmanager').addClass('glyphicon glyphicon-asterisk');
               //    }
               //});


               this.chrAddress = ko.computed(function ()
               {
                   if (self.BuildingAddress.length > 0)
                   {
                       $('#idAddress').css("color", "green");
                       $('#idAddress').removeClass('glyphicon glyphicon-asterisk');
                       $('#idAddress').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingAddress.length;
                   } else
                   {
                       $('#idAddress').removeClass('glyphicon glyphicon-ok');
                       $('#idAddress').css("color", "red");
                       $('#idAddress').addClass('glyphicon glyphicon-asterisk');
                   }
               });
               //City
               this.chrCity = ko.computed(function ()
               {
                   if (self.BuildingCity.length > 0)
                   {
                       $('#idCity').css("color", "green");
                       $('#idCity').removeClass('glyphicon glyphicon-asterisk');
                       $('#idCity').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingCity.length;
                   }
                   else {
                       $('#idCity').removeClass('glyphicon glyphicon-ok');
                       $('#idCity').css("color", "red");
                       $('#idCity').addClass('glyphicon glyphicon-asterisk');
                   }
               });

               this.chrState = ko.computed(function ()
               {
                   if (self.BuildingState.length > 0)
                   {
                       $('#idState').css("color", "green");
                       $('#idState').removeClass('glyphicon glyphicon-asterisk');
                       $('#idState').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingState.length;
                   }
                   else {
                       $('#idState').removeClass('glyphicon glyphicon-ok');
                       $('#idState').css("color", "red");
                       $('#idState').addClass('glyphicon glyphicon-asterisk');
                   }
               });
              
               
               
               this.chrPhone = ko.computed(function () {
                   if (self.BuildingPhone.length > 0) {
                       $('#idPhone').css("color", "green");
                       $('#idPhone').removeClass('glyphicon glyphicon-asterisk');
                       $('#idPhone').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingPhone.length;
                   }
                   else {
                       $('#idPhone').removeClass('glyphicon glyphicon-ok');
                       $('#idPhone').css("color", "red");
                       $('#idPhone').addClass('glyphicon glyphicon-asterisk');
                   }
               });

               this.chrFax = ko.computed(function () {
                   if (Fax().length > 0) {
                       $('#idFax').css("color", "green");
                       $('#idFax').removeClass('glyphicon glyphicon-asterisk');
                       $('#idFax').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Fax().length;
                   }
                   else {
                       $('#idFax').removeClass('glyphicon glyphicon-ok');
                       $('#idFax').css("color", "red");
                       $('#idFax').addClass('glyphicon glyphicon-asterisk');
                   }
               });

               this.chrZipcode = ko.computed(function () {
                   if (self.BuildingZipcode.length > 0) {
                       $('#idZipcode').css("color", "green");
                       $('#idZipcode').removeClass('glyphicon glyphicon-asterisk');
                       $('#idZipcode').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + self.BuildingZipcode.length;
                   }
                   else {
                       $('#idZipcode').removeClass('glyphicon glyphicon-ok');
                       $('#idZipcode').css("color", "red");
                       $('#idZipcode').addClass('glyphicon glyphicon-asterisk');
                   }
               });
               this.chrEmail = ko.computed(function () {
                   if (Email().length > 0) {
                       $('#idEmail').css("color", "green");
                       $('#idEmail').removeClass('glyphicon glyphicon-asterisk');
                       $('#idEmail').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Email().length;
                   }
                   else {
                       $('#idEmail').removeClass('glyphicon glyphicon-ok');
                       $('#idEmail').css("color", "red");
                       $('#idEmail').addClass('glyphicon glyphicon-asterisk');
                   }
               });
        //  self.BuildingName = ko.observable();
               //self.BuildingAddress = ko.observable();
               //self.BuildingCity = ko.observable();
               //self.BuildingState = ko.observable();
               //self.BuildingZipcode = ko.observable();
               //self.BuildingPhone = ko.observable();
               this.btnStat = ko.computed(function () {
                   //debugger;
                   if (self.BuildingState.length > 0 &&
                       self.BuildingName.length > 0 &&
                      // Manager().length > 0 &&
                       self.BuildingAddress.length > 0 &&
                      self.BuildingCity.length > 0 &&
                       self.BuildingPhone.length > 0 &&
                       self.BuildingZipcodeFse.length > 0)
                   {
                       $('#submitlgmd').prop("disabled", false);
                       $('#submitlgmd_B').prop("disabled", false);
                   }
                   else
                   {
                       $('#submitlgmd').prop("disabled", true);
                       $('#submitlgmd_B').prop("disabled", true);
                   }
               }, this);

                
               this.name = ko.computed(function () { return Name($('#ideditname').val()); });
               this.address = ko.computed(function () { return Address($('#ideditaddress').val()); });
               this.city = ko.computed(function () { return City($('#ideditcity').val()); });
               this.state = ko.computed(function () { return State($('#ideditstate').val()); });
               this.zipcode = ko.computed(function () { return Zipcode($('#ideditzipcode').val()); });
               //this.aptnum = ko.computed(function () { return AptNum($('#ideditaptnum').val()); });
               this.phone = ko.computed(function () { return Phone($('#ideditphone').val()); });
               //this.manager = ko.computed(function () { return Manager($('#ideditmanager').val()); });



           };
           
           ko.applyBindings(new ViewModel);

          
          

       

    //only numeric handler
    ko.bindingHandlers.numeric = {
        init: function (element, valueAccessor) {
            $(element).on("keydown", function (event) {
                // Allow: backspace, delete, tab, escape, and enter
                if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                    // Allow: Ctrl+A
                    (event.keyCode == 65 && event.ctrlKey === true) ||
                    // Allow: . ,
                    (event.keyCode == 188 || event.keyCode == 190 || event.keyCode == 110) ||
                    // Allow: home, end, left, right
                    (event.keyCode >= 35 && event.keyCode <= 39)) {
                    // let it happen, don't do anything
                    return;
                }
                else {
                    // Ensure that it is a number and stop the keypress
                    if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                        event.preventDefault();
                    }
                }
            });
        }
    };
    


    





});