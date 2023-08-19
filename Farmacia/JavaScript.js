function AllowOnlySpanishChars(e) {
    var keyCode = e.keyCode || e.which;
    var keyChar = String.fromCharCode(keyCode);

    // Permite solo caracteres alfabéticos, números, espacios y la letra "ñ" en mayúscula y minúscula
    var allowedChars = /^[a-zA-Z0-9ñÑ\s]+$/;

    if (!allowedChars.test(keyChar)) {
        e.preventDefault();
        return false;
    }
}