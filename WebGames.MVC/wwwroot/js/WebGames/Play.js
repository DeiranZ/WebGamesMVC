const element = document.getElementById('game');

let gameFrame = document.getElementsByClassName('ZMgameFrame')[0];
let ogWidth = gameFrame.style.width;
let ogHeight = gameFrame.style.height;


document.getElementById('fullscreen-button').addEventListener('click', () => {
	if (screenfull.isEnabled) {
		screenfull.request(element, { navigationUI: 'hide' });
	}
});

if (screenfull.isEnabled) {
	screenfull.on('change', () => {
		if (screenfull.isFullscreen) {
			gameFrame.style.width = "100%"
			gameFrame.style.height = "100%"
		}
		else {
			gameFrame.style.width = ogWidth;
			gameFrame.style.height = ogHeight;
		}
	});
}