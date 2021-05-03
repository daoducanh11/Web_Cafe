//Trang chủ Admin
// ===== Scroll to Top ==== 
var x = document.getElementById('return-to-top');
$(window).scroll(function () {
    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
        x.style.opacity = 1;
    }
    else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
        x.style.opacity = 0;
    }
});
$('#return-to-top').click(function () {      // When arrow is clicked
    $('body,html').animate({
        scrollTop: 0                       // Scroll to top of body
    }, 500);
});

//Trang thông tin sản phẩm
// var t = document.getElementById("photo");
// t.forEach(function myFun(item, index) {
//     var img = document.createElement("img");
//     img.src = item.name;
// src = getElementById("gamediv");
// src.appendChild(this.img)
//   document.getElementById("demo").innerHTML += index + ":" + item + "<br>"; 
// })


// $(function () {
//     // Summernote
//     $('#proHighlight').summernote()

//     $('#proDescription').summernote()

//     //Date range picker with time picker
//     $('#timePromo').daterangepicker({
//         timePicker: true,
//         timePickerIncrement: 30,
//         locale: {
//             format: 'DD/MM/YYYY hh:mm A'
//         }
//     })
// })

// var previewNode = document.querySelector("#template");
// previewNode.id = "";
// var previewTemplate = previewNode.parentNode.innerHTML;
// previewNode.parentNode.removeChild(previewNode);

// var myDropzone = new Dropzone(document.body, { // Make the whole body a dropzone
//     url: "/target-url", // Set the url
//     thumbnailWidth: 80,
//     thumbnailHeight: 80,
//     parallelUploads: 20,
//     previewTemplate: previewTemplate,
//     autoQueue: false, // Make sure the files aren't queued until manually added
//     previewsContainer: "#previews", // Define the container to display the previews
//     clickable: ".fileinput-button" // Define the element that should be used as click trigger to select files.
// });
// // Update the total progress bar
// myDropzone.on("totaluploadprogress", function (progress) {
//     document.querySelector("#total-progress .progress-bar").style.width = progress + "%";
// });

// myDropzone.on("sending", function (file) {
//     // Show the total progress bar when upload starts
//     document.querySelector("#total-progress").style.opacity = "1";
//     // And disable the start button
//     file.previewElement.querySelector(".start").setAttribute("disabled", "disabled");
// });

// // Hide the total progress bar when nothing's uploading anymore
// myDropzone.on("queuecomplete", function (progress) {
//     document.querySelector("#total-progress").style.opacity = "0";
// });
// document.querySelector("#actions .start").onclick = function () {
//     myDropzone.enqueueFiles(myDropzone.getFilesWithStatus(Dropzone.ADDED));
// };
// document.querySelector("#actions .cancel").onclick = function () {
//     myDropzone.removeAllFiles(true);
// };