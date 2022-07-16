//#region Elements
var pass = document.getElementById("regPass");
var passConf = document.getElementById("regPassConf");
var selComp = document.getElementById("Company");
var selPost = document.getElementById("Post");
var email = document.getElementById("regEmail");
var check = true;
//#endregion
//#region functions
function showPassMatch() {
	document.getElementById("passMatch").style.display = "block";
}
function showPassEmpty() {
	document.getElementById("passEmpty").style.display = "block";
}
function hidePass() {
	document.getElementById("passMatch").style.display = "none";
	document.getElementById("passEmpty").style.display = "none";
}
function showSelect() {
	document.getElementById("select").style.display = "block";
}
function hideSelect() {
	document.getElementById("select").style.display = "none";
}
function showEmail() {
	document.getElementById("emailCheck").style.display = "block";
}
function hideEmail() {
	document.getElementById("emailCheck").style.display = "none";
}
function checkPass() {
	if (pass.value === "") {
		if (passConf.value === "") {
			hidePass();
			return;
		}
		hidePass();
		showPassEmpty();
	}
	else {
		if (passConf.value === "") {
			showPassEmpty();
			return;
		}
		else if (pass.value === passConf.value) {
			hidePass();
			return;
		}
		hidePass();
		showPassMatch();
	}
}
function checkSelect() {
	if (selComp.value === "" || selPost.value === "") {
		showSelect();
		return;
	}
	hideSelect();
}
function checkEmail() {
	if (email.value.includes("@") && email.value.includes(".")) {
		hideEmail();
		return;
	}
	showEmail();
}
//#endregion
//#region events
pass.onkeyup = function () { checkPass(); };
passConf.onkeyup = function () { checkPass(); };
selComp.onblur = function () { checkSelect() };
selPost.onblur = function () { checkSelect() };
email.onkeyup = function () { checkEmail() };
//#endregion