﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

///////////////////////////// show specs in category
// get reference to button
var showbtn = document.getElementById("showSpec");
// add event listener for the button, for action "click"
var spec_index = 0;
if (showbtn != null) {
    showbtn.addEventListener("click", addChild);
    function addChild() {
        var spec = "<table id='spec" + spec_index + "' class= 'table table-light table - borderless table - hover text - center mb - 0' >" +
            "<tbody class='align-middle' >" +
            "<tr>" +
            "<td>" +
            "<select class='form-control Specs' >" +
            "<option selected> United States </option>" +
            "<option > Afghanistan </option>" +
            "<option > Albania </option>" +
            "<option > Algeria </option>" +
            "</select> </td>" +
            "<td class= 'align-middle' > specification </td>" +
            "<td class= 'align-middle' >" +
            "<button type='button' id='rem" + spec_index + "'  class= 'btn btn-sm btn-danger rem' >" +
            "<i class='fa fa-times' > </i>" +
            "</button >" +
            "</td></tr> </tbody > </table>"
        $("#spec").append(spec);
        let div = document.getElementById("spec")
        let rembtn = document.querySelector("#rem" + spec_index).addEventListener("click",
            function () {
                // let confirm=confirm("are you sure?");
                let rem_parent = this.parentNode.parentNode.parentNode.parentNode;
                // console.log(rem_parent);

                div.removeChild(rem_parent);



            });

        spec_index += 1;

    }
}
///////////////////////show specs in product

// get reference to button
var showbtnprd = document.getElementById("showSpecProd");
// add event listener for the button, for action "click"
console.log(showbtnprd);
if (showbtnprd != null) {
    var specprod_index = 0;
    showbtnprd.addEventListener("click", addChildProd);
    function addChildProd() {
        var spec = "<table id='spec" + spec_index + "' class= 'table table-light table - borderless table - hover text - center mb - 0' >" +
            "<tbody class='align-middle' >" +
            "<tr>" +
            "<td>" +
            "<select class='form-control Specs ' >" +
            "<option selected> United States </option>" +
            "<option > Afghanistan </option>" +
            "<option > Albania </option>" +
            "<option > Bulgeria </option>" +
            "</select> </td>" +
            "<td class= 'align-middle' > specification </td>" +
            "<td class= 'align-middle' >" +

            "</td></tr>" +
            "<tr>" +
            "<td>" +
            "<input class='form-control Specs' >" +

            "</input> </td>" +
            "<td class= 'align-middle' > Value </td>" +
            "<td class= 'align-middle' >" +
            "<button type='button' id='rem" + spec_index + "'  class= 'btn btn-sm btn-danger rem' >" +
            "<i class='fa fa-times' > </i>" +
            "</button >" +
            "</td></tr>" +
            " </tbody > </table>"
        $("#spec").append(spec);
        let div = document.getElementById("spec")
        let rembtn = document.querySelector("#rem" + specprod_index).addEventListener("click",
            function () {
                // let confirm=confirm("are you sure?");
                let rem_parent = this.parentNode.parentNode.parentNode.parentNode;
                // console.log(rem_parent);

                div.removeChild(rem_parent);



            });

        specprod_index += 1;

    }
}

/////////////////////
$(document).ready(function () {


    let del = document.querySelector(".dele");
    $(del).each(function () {

        $(this).on('click', function (e) {
            e.preventDefault();
            let link = $(this);
            let target = $(this).attr("href");
            Swal.fire({

                title: 'Are you sure you wish to delete?',
                text: 'this action is irreversable',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, I am sure',
                cancelButtonText: 'Noooo!'
            }).then((willDelete) => {
                if (willDelete) {
                    Notify("deleted");
                }
                else {
                    Notify("not deleted")
                }
            });
        });
    });
});







///////////alerts
/////////////////////

/*   (function ($) {
        showSwal = function (type) {
            'use strict';
            if (type === 'basic') {
                swal({
                    text: 'Any fool can use a computer',
                    button: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary"
                    }
                })

            } else if (type === 'title-and-text') {
                swal({
                    title: 'Read the alert!',
                    text: 'Click OK to close this alert',
                    button: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary"
                    }
                })

            } else if (type === 'success-message') {
                swal({
                    title: 'Congratulations!',
                    text: 'You entered the correct answer',
                    icon: 'success',
                    button: {
                        text: "Continue",
                        value: true,
                        visible: true,
                        className: "btn btn-primary"
                    }
                })

            } else if (type === 'auto-close') {
                swal({
                    title: 'Auto close alert!',
                    text: 'I will close in 2 seconds.',
                    timer: 2000,
                    button: false
                }).then(
                    function () { },
                    // handling the promise rejection
                    function (dismiss) {
                        if (dismiss === 'timer') {
                            console.log('I was closed by the timer')
                        }
                    }
                )
            } else if (type === 'warning-message-and-cancel') {
                swal({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3f51b5',
                    cancelButtonColor: '#ff4081',
                    confirmButtonText: 'Great ',
                    buttons: {
                        cancel: {
                            text: "Cancel",
                            value: null,
                            visible: true,
                            className: "btn btn-danger",
                            closeModal: true,
                        },
                        confirm: {
                            text: "OK",
                            value: true,
                            visible: true,
                            className: "btn btn-primary",
                            closeModal: true
                        }
                    }
                })

            } else if (type === 'custom-html') {
                swal({
                    content: {
                        element: "input",
                        attributes: {
                            placeholder: "Type your password",
                            type: "password",
                            class: 'form-control'
                        },
                    },
                    button: {
                        text: "OK",
                        value: true,
                        visible: true,
                        className: "btn btn-primary"
                    }
                })
            }
        }

    })(jQuery);*/