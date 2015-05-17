$(function () {


            
    function ViewModel() {
       
        var buildingID = $('#buildingidforcustomjs').attr("data-buildingid");
       


        var self = this;
        
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
                   if (Name().length > 0  ) {
                       $('#idName').css("color", "green");
                       $('#idName').removeClass('glyphicon glyphicon-asterisk');
                       $('#idName').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Name().length;
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
                   if (Address().length > 0)
                   {
                       $('#idAddress').css("color", "green");
                       $('#idAddress').removeClass('glyphicon glyphicon-asterisk');
                       $('#idAddress').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Address().length;
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
                   if (City().length > 0)
                   {
                       $('#idCity').css("color", "green");
                       $('#idCity').removeClass('glyphicon glyphicon-asterisk');
                       $('#idCity').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Address().length;
                   }
                   else {
                       $('#idCity').removeClass('glyphicon glyphicon-ok');
                       $('#idCity').css("color", "red");
                       $('#idCity').addClass('glyphicon glyphicon-asterisk');
                   }
               });

               this.chrState = ko.computed(function ()
               {
                   if (State().length > 0)
                   {
                       $('#idState').css("color", "green");
                       $('#idState').removeClass('glyphicon glyphicon-asterisk');
                       $('#idState').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + State().length;
                   }
                   else {
                       $('#idState').removeClass('glyphicon glyphicon-ok');
                       $('#idState').css("color", "red");
                       $('#idState').addClass('glyphicon glyphicon-asterisk');
                   }
               });
              
               
               
               this.chrPhone = ko.computed(function () {
                   if (Phone().length > 0) {
                       $('#idPhone').css("color", "green");
                       $('#idPhone').removeClass('glyphicon glyphicon-asterisk');
                       $('#idPhone').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Phone().length;
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
                   if (Zipcode().length > 0) {
                       $('#idZipcode').css("color", "green");
                       $('#idZipcode').removeClass('glyphicon glyphicon-asterisk');
                       $('#idZipcode').addClass('glyphicon glyphicon-ok');
                       return 'chars: ' + Zipcode().length;
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
               //  
               this.btnStat = ko.computed(function () {
                   //debugger;
                   if (State().length > 0 &&
                       Name().length >0 &&
                      // Manager().length > 0 &&
                       Address().length > 0 &&
                       City().length > 0 &&
                       Phone().length > 0 &&
                       Zipcode().length > 0)
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