function check_all() {
    if (!$("#check_all").is(":checked"))
       $(".checkbox").prop('checked',false);
   else
       $(".checkbox").prop('checked',true);
}

$(document).ready(function() {
    $("#check-all-btn").click(function () {
        if ($("#check_all").is(':checked'))
            $(".check_all").prop('checked',false);
        else
            $(".check_all").prop('checked',true);
            check_all();
    });
});



$(document).ready(function() {
    $("#check_all").click(check_all());
});

$(document).ready(function() {
    $(".input-click").click(function() {
        var arrayOfInputs = document.getElementsByClassName("checkboxindex");
        for(var i = 0;i<arrayOfInputs.length;i++)
        {
            if ($(arrayOfInputs[i]).is(":checked")){
                $('.btn-input').prop("hidden",false);
                return;
            }
            else 
                $('.btn-input').prop('hidden',true);
        }
    });
});

