self.addEventListener('push', function (event) {
    //console.log('[Service Worker] Push Received.');
    console.log(`[Service Worker] Push had this data: "${event.data.text()}"`);//MANEJO DEL MENSAJE DE RESPUESTA

    const title = 'Nueva casa ingresada';
    const options = {
        body: event.data.text(),
        icon: '../Media/logo-casas-red.png',
        badge: 'images/badge.png'
    };

    event.waitUntil(self.registration.showNotification(title, options));
});
self.addEventListener('notificationclick', function (event) {
    console.log('[Service Worker] Notification click Received.');

    event.notification.close();

    event.waitUntil(
        clients.openWindow('https://developers.google.com/web/')
    );
});
