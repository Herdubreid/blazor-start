import './style.scss';
import Welcome from './Welcome.svelte';

window.welcome = {
    init: (tag) => {
        const target = document.getElementsByTagName(tag)[0];
        Welcome = new Welcome({
            target,
            props: {}
        });
    }
}
