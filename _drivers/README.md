# SIMCOM USB Driver para módulo 4G

## Windows (Win10/Win11)

**A7670/A7672/A7600C-L1**
- Por favor, use A7600X-Windows-Driver.7z para instalar o Driver USB
- O driver conterá pelo menos 2 portas UART e driver RNDIS (Internet):
    - AT Command Port
    - Diagnostic Port
    - NMEA Port - GPS (Optinal)

**SIM7600**
- Use SIM7600_Windows10_Driver.zip para instalar o driver USB
- O driver conterá pelo menos 3 portas UART e driver RNDIS (Internet):
    - AT Command Port
    - Diagnostic Port
    - NMEA Port - GPS


## Linux (Ubuntu/Debian/...)

- O driver já foi instalado com o driver: ECM/EEM/RNDIS/NDIS/QMI
- Para abrir a internet no Linux:
- Comando para verificar a interface USB:

```sh
ip a
```

E verifique a interface *usb0*

- Adquirir IP (DHCP) - Opcional se o IP não for detectado automaticamente:


```sh
sudo dhclient -v usb0
```

- Adquirir DNS - Opcional

```sh
sudo route add -net 0.0.0.0 usb0
```
