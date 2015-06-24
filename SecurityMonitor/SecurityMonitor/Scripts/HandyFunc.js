self.UsernameTenant.subscribe(function (input) {
    var result = isValidEmailAddress(input);
    if (result == true)
    {
        $.ajax(
            {
                type: "GET",
                url: '/building/Searchforusername/',
                dataType: "json",
                data: { search: input },
                success: function (data) {

                    var jsonresult = JSON.stringify(data.Data);// Json.stringify make an object into a json string
                    //alert(jsonresult);
                    var TrkPkgs = JSON.parse(jsonresult); //JSON.parse makes a json string into a json object example obj.FirstName
                    for (var i = 0; i < TrkPkgs.length; i++) {
                        if (TrkPkgs[i].Email == input) {
                            $('#validateemailid').html("<span>This email address is already in used");
                        }
                    }
                }
            });
    }
    else if (input == "") {
        $('#validateemailid').html("");
    }
    else {
        $('#validateemailid').html("<span>invalid Email Address!");
    }
    console.log(input + " ");
});