window.setAuthenticatedUser = (username) => {
    localStorage.setItem("auth_user", username);
};

window.getAuthenticatedUser = () => {
    return localStorage.getItem("auth_user") || null;
};

window.clearAuthenticatedUser = () => {
    localStorage.removeItem("auth_user");
};
