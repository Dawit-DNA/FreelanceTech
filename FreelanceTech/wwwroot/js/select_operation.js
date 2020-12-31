$(document).ready(function () {
    alert("value0");
    $("select.category").change(function fun() {
        alert("value");
        let value = $("#category option:selected").val();
        switch (value) {
            case "Web Development": {
                $('select.skills').children().remove();
                $('#skills').append(`<option value="FullStack">Full Stack Web Developer</option>`);
                $('#skills').append(`<option value="FRND">Front End Developer</option>`);
                $('#skills').append(`<option value="WP">CMS Development</option>`);
                $('#skills').append(`<option value="BED">Back End Developer</option>`);
                $('#skills').append(`<option value="dotnetcore">.NET Core</option>`);
                $('#skills').append(`<option value="njs">Nodejs</option>`);
                $('#skills').append(`<option value="APID">API Development</option>`);
                $('#skills').append(`<option value="APII">API Integration</option>`);
                $('#skills').append(`<option value="Vj">VueJs</option>`);
                $('#skills').append(`<option value="Dj">Django</option>`);
                $('#skills').append(`<option value="React">React</option>`);
                $('#skills').append(`<option value="Angular">AngularJs</option>`);
                break;
            }
            case "Mobile Development": {
                $('select.skills').children().remove();
                $('#skills').append(`<option value="swift">iOS Development(swift)</option>`);
                $('#skills').append(`<option value="Java">Android Development(Java)</option>`);
                $('#skills').append(`<option value="Java">Android Development(Kotlin)</option>`);
                $('#skills').append(`<option value="flutter">Flutter</option>`);
                $('#skills').append(`<option value="ionic">Ionic</option>`);
                $('#skills').append(`<option value="ReactN">React Native</option>`);
                $('#skills').append(`<option value="xamarin">Xamarin</option>`);
                $('#skills').append(`<option value="apache">Apache Cordova</option>`);
                break;
            }
            case "Windows App Development": {
                $('select.skills').children().remove();
                $('#skills').append(`<option value="WPF">WPF</option>`);
                $('#skills').append(`<option value="C">C++</option>`);
                $('#skills').append(`<option value="WPF">UWP</option>`);
                $('#skills').append(`<option value="WF">WinForms</option>`);
                break;
            }
            case "Grarphics": {
                $('select.skills').children().remove();
                $('#skills').append(`<option value="logo">Logo Designer</option>`);
                $('#skills').append(`<option value="VDE">Video Editing</option>`);
                $('#skills').append(`<option value="ADP">Adobe Photoshop</option>`);
                $('#skills').append(`<option value="ADI">Adobe Illustrator</option>`);
                $('#skills').append(`<option value="3D">3D Design</option>`);
                $('#skills').append(`<option value="3DM">3D Modelling</option>`);
                $('#skills').append(`<option value="ADCS">Adobe Creative Suit</option>`);
                break;
            }

            default:
                break;
        }
    });
});