
var isSubscribed;
if ('serviceWorker' in navigator && 'PushManager' in window) {
    console.log('Service Worker and Push is supported');

    navigator.serviceWorker.register('../Scripts/Global/sw.js')
        .then(function (swReg) {
            console.log('Service Worker is registered', swReg);

            swRegistration = swReg;
        })
        .catch(function (error) {
            console.error('Service Worker Error', error);
        });
} else {
    console.warn('Push messaging is not supported');
    pushButton.textContent = 'Push Not Supported';
}
const applicationServerPublicKey =  'BJH4dh04OJMDVlLQbzEsOesntGWGzuekxdP-yjq2wc6VWSJI3dmKJC4XsGhMaWwAT82xGiyFTkJ8FQoUz0zEFtU';

function subscribeUser() {
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    swRegistration.pushManager.subscribe({
        userVisibleOnly: true,
        applicationServerKey: applicationServerKey
    })
        .then(function (subscription) {
            console.log('User is subscribed:', subscription);

            updateSubscriptionOnServer(subscription);

            isSubscribed = true;

            updateBtn();
        })
        .catch(function (err) {
            console.log('Failed to subscribe the user: ', err);
            updateBtn();
        });
}


function initialiseUI() {
    // Set the initial subscription value
    swRegistration.pushManager.getSubscription()
        .then(function (subscription) {
            isSubscribed = !(subscription === null);

            if (isSubscribed) {
                console.log('User IS subscribed.');
            } else {
                console.log('User is NOT subscribed.');
            }

            updateBtn();
        });
}

function updateBtn() {
    if (isSubscribed) {
        console.log('Disable Push Messaging');
    } else {
        console.log('Enable Push Messaging');
    }

    //pushButton.disabled = false;
}


//const title = 'Push Codelab';
//const options = {
//    body: 'Yay it works.',
//    icon: 'images/icon.png',
//    badge: 'images/badge.png'
//};
//self.registration.showNotification(title, options);


/*function requestPermission() {
    return new Promise(function (resolve, reject) {
        const permissionResult = Notification.requestPermission(function (result) {
            // Handling deprecated version with callback.
            resolve(result);
        });

        if (permissionResult) {
            permissionResult.then(resolve, reject);
        }
    })
        .then(function (permissionResult) {
            if (permissionResult !== 'granted') {
                throw new Error('Permission not granted.');
            }
        });
}
var source;
requestPermission().then(function () {
    source = new EventSource('webapi.casasred.com/api/Notificacion/ChatCallbackMsg/'); 
    source.onmessage = function (e) {
     var data = e.data.split('|');
     
    };
    }
);
*/
function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
        .replace(/\-/g, '+')
        .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

function updateSubscriptionOnServer(subscription) {
    // TODO: Send subscription to application server

    const subscriptionJson = document.querySelector('.js-subscription-json');
    if (subscription) {
        console.log(JSON.stringify(subscription));
        //subscriptionDetails.classList.remove('is-invisible');
    } else {
        //subscriptionDetails.classList.add('is-invisible');
    }
}
