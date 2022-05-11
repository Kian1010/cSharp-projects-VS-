function toggle(num){
    let id = document.getElementById(num);

    if (id.style.display === "none"){
        id.style.display = "block";
    }
    else{
        id.style.display = "none";
    }
}

function clearall() {
     let checkboxes = document.getElementsByName('chkbx');
    for(let i=0;i<checkboxes.length;i++){
        if (checkboxes[i].type == 'checkbox'){
            checkboxes[i].checked=false;
        }
    }

        document.getElementById('productTxt').value= '';

        let txtbx = document.getElementsByClassName('txtbx');
        
        for (let x=0;x<txtbx.length;x++){
            if (txtbx[x].type == 'text'){
                txtbx[x].value = '';
            }
        }

        document.getElementById("submitData").innerHTML = '';
 }

 let count = 0;
 function submit(){
     document.getElementById("submitData").innerHTML = "Your data has been submitted!";

     

     if ( (document.getElementById("submitData").innerHTML == "Your data has been submitted!") || 
     (document.getElementById("submitData").innerHTML == "Your data has been submitted! x" +count) ){
         count=count+1;
            //counts the amount of times the user pressed the submit button
        if (count !=1){
         document.getElementById("submitData").innerHTML = "Your data has been submitted! x"+count;
        }

     }
 }

