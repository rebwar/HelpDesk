
    //----< events >----

    $("#select_and_upload_button").click(function () { $("#files_input_field").click(); }); 

 
   //--------< functions >--------

    function upload_files() {

        //------< upload_files() >------

        //--< get selected files >--

        var fileUpload = $("#files_input_field").get(0);

        var files = fileUpload.files;

        if (files.length == 0) return;

 

        //< load into FormData >

        var data = new FormData();

        for (var i = 0; i < files.length; i++) {

            data.append(files[i].name, files[i]);

        }

        //</ load into FormData >

        //--</ get selected files >--

 

        //--< ajax upload >--

        $.ajax({

            type: "POST",

            url: "Admin/AddArticle",

            contentType: false,  //default: 'application/x-www-form-urlencoded; charset=UTF-8'

            processData: false,  //default: data, other DOMDocument

            data: data,          //Type: PlainObject or String or Array
           success: function (message) {

                alert(message);
            },
            error: function () {
                alert("There was error uploading files!");

            }

        });

        //------</ upload_files() >------

    }

        //--------</ functions >--------

