const usersContainer = document.querySelector('.users-container');

const getUserInfo = async () => {
    try {
        const response = await fetch('https://localhost:7290/admin/getusers');
        const users = await response.json();
        console.log(users);
        users.forEach(user => {
            const userCard = document.createElement('div');
            userCard.classList.add('user');
            const userId = document.createElement('div');
            userId.classList.add('user-id');
            userId.innerHTML = `<span>${user.id}</span>`;
            userCard.appendChild(userId);
            const userEmail = document.createElement('div');
            userEmail.classList.add('user-email');
            userEmail.innerHTML = `<span>${user.email}</span>`;
            userCard.appendChild(userEmail);
            const deleteUserBtn = document.createElement('input');
            deleteUserBtn.type = 'submit';
            deleteUserBtn.value = 'Delete';
            deleteUserBtn.classList.add('btn', 'btn-danger');
            deleteUserBtn.addEventListener('click', () => {
                console.log('deleteUser', user.id);
                try {
                    fetch(`https://localhost:7290/admin/deluser?userID=${user.id}`, {
                        method: 'POST',
                    });
                } catch (err) {
                    console.error('Error:', err);
                }
            });
            userCard.appendChild(deleteUserBtn);
            usersContainer.appendChild(userCard);
        });

    } catch (err) {
        console.error('Error:', err);
    }
};

getUserInfo();