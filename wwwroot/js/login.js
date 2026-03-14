document.addEventListener("DOMContentLoaded", function () {

    const form = document.getElementById("loginForm");

    form.addEventListener("submit", function (e) {
        e.preventDefault();

        const userId = document.getElementById("userId").value;
        const password = document.getElementById("password").value;

        if (userId === "" || password === "") {
            alert("Please fill all fields");
            return;
        }
        $(this).prop("disabled", true);
        $.ajax({
            url: "/Login/Login",
            type: "POST",
            data: {
                UserId: userId,
                Password: password
            },
            success: function (response) {
                console.log(response);
                alert("Login Success");
                window.location.href = "/UserMaster/ListAll";
                $(this).prop("disabled", false);
            },
            error: function (error) {
                alert("Login Failed");
                console.log("===> eeror" +error);
                $(this).prop("disabled", false);
            }
        });

    });

});
