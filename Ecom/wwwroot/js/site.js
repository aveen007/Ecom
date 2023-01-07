// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
/*
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
            "<option selected hidden> Select One </option>";
        alert(specifications[0])
        for (var specification in specifications) {
            alert(specification.SpecificationName);
            spec += "<option value=" + specification.Id + ">" + specification.SpecificationName + "</option > ";
        }
            spec += "<option> United States </option>" +
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
}*/
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

//////////////////////
$(document).ready(function () {


    let showSpec = document.querySelector("#showSpec")
    if (showSpec ) {
      
        showSpec.addEventListener("click",

      async function (e) {
        e.preventDefault();
        let link = $(this);
          let target = $(this).attr("href");
          var temp = document.getElementById("specIds")
          var CatSpecs = []
          if (temp) {
              CatSpecs = temp.value.substring(1, temp.value.length - 1).split(',')
          }
        var tmp = "";
        for (i = 0; i < specifications.length; i++) {
            tmp += '<input type="checkbox" id="spec' + specifications.options[i].value + '" name="spec' + specifications.options[i].value + '" value=' + specifications.options[i].value + ' class="spec_check"'
            if (CatSpecs.includes('"' + specifications.options[i].value + '"')) {
                tmp += ' checked'
            }
            tmp += '> <label for="spec' + specifications.options[i].value + '"> ' + specifications.options[i].text + '</label><br>'
        }
        const { value: formValues } =await  Swal.fire({
            title: 'Multiple inputs',
            html:
                '<form action="/action_page.php">' +
                tmp +
                '</form>',
            focusConfirm: false,
            preConfirm: () => {
                var checkedValue = [];
                var inputElements = document.getElementsByClassName('spec_check');
                for (var i = 0; inputElements[i]; ++i) {
                    if (inputElements[i].checked) {
                        checkedValue.push(inputElements[i].value);
                            
                          
                    }
                }
                
                return checkedValue

                    
            }
        })
        if (formValues) {
            var tmp = document.getElementById("specIds")
                if (tmp) {
                    let div = document.getElementById("spec")
                    div.removeChild(tmp)
            }
            var spec = "<input hidden id='specIds' name='Specifications' value='" + JSON.stringify(formValues) + "'>"
            $("#spec").append(spec);
            if (isProduct) {
                updateValuesInput();
            }
            updateSpecs();
        }


        });
    }
});

function updateSpecs() {
    var spec = document.getElementById("spec")
    var specs_list = document.getElementById("specs_list")

    if (specs_list) {
        spec.removeChild(specs_list);
        var tmp_div = "<div id='specs_list' class='form-group'></div>";
        $("#spec").append(tmp_div);
    }

    var specIds = document.getElementById("specIds")
    var specValues = document.getElementById("specValues");
    if (specIds && specIds.value.length > 2) {
        var label = "<label class='control-label'>Specifications:</label>"
        $("#specs_list").append(label);

        var tmp = specIds.value.substring(1, specIds.value.length - 1);
        var tmps = tmp.split(',');
        if (isProduct) {
            var value_dic_tmp = {};
        }
        for (var i = 0; i < tmps.length; i++) {
            tmps[i] = tmps[i].substring(1, tmps[i].length - 1);
            var tmp_spec = "<div id='spec_" + tmps[i] + "' style='border-radius: 25px;border: 2px solid Black;margin: 5px;padding: 10px;width: fit-content;'>" + spec_dictionary[JSON.stringify(tmps[i])];
            if (isProduct) {
                tmp_spec += "<input class='form-control values' oninput='updateValuesInput()' ";
                if (value_dictionary[tmps[i]]) {
                    value_dic_tmp[tmps[i]] = value_dictionary[tmps[i]];
                    tmp_spec += "value='" + value_dictionary[tmps[i]] + "'";
                }
                else {
                    value_dic_tmp[tmps[i]] = "";
                }
                tmp_spec += " required>";
            }
            tmp_spec += "</div>";
            $("#specs_list").append(tmp_spec);
        }
        if (isProduct) {
            value_dictionary = value_dic_tmp;
        }

    }
}


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