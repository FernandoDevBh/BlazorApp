window.ShowToastr = (type, message) => {
    switch (type) {
        case "success":
            return toastr.success(message, 'Operation Successful', { timeOut: 5000 });
        default:
            return toastr.error(message, 'Operation Failed', { timeOut: 5000 });
    }
};

window.SweetAlert = (type, title, text) => {
    switch (type) {
        case 'success':
            return swal.fire(
                title,
                text,
                'success'
            );
        default:
            return swal.fire(
                title,
                text,
                'error'
            );
    }
}