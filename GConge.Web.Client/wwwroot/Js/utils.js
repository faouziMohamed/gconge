/**
 * @param {string} modalId
 */
export function CloseModal(modalId) {
    var myModalEl = document.getElementById(modalId);
    var modal = bootstrap?.Modal?.getInstance(myModalEl);
    modal?.hide()
}
