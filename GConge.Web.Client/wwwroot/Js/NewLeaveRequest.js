export function CloseModal() {
    var myModalEl = document.getElementById('AddNewLeave');
    var modal = bootstrap?.Modal?.getInstance(myModalEl);
    modal?.hide()
}
