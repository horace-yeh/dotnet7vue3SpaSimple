import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import mkcert from 'vite-plugin-mkcert'

// https://github.com/vuetifyjs/vuetify-loader/tree/next/packages/vite-plugin
import vuetify from 'vite-plugin-vuetify'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
		vue(),
		mkcert(),
		vuetify({ autoImport: true }),
	],
    server: {
        port: 44417,
        https: true,
        strictPort: true,
        proxy: {
            '/api': {
                target: 'https://localhost:7208',
                changeOrigin: true,
                secure: false,
                rewrite: (path) => path.replace(/^\/api/, '/api')
            }
        }
    }
})
