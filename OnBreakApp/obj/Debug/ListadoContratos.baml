   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=nullZ=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.MetroMahApps.Metro.Behaviours  5   	   V;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   G CPresentacion, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:OnBreakApp
OnBreakApp 5	   	   !   OnBreakApp.ListadoContratos   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   N
behaviours=clr-namespace:MahApps.Metro.Behaviours;assembly=MahApps.Metro  5   	   JControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   $localclr-namespace:OnBreakApp  5	   	     � Title$%  OnBreak - Eventos Empresariales��5   	   
���   �$	��700��6D   $
��1300��6Q    � WindowStartupLocation$ CenterScreen=�6^   .��5      � $��8,0,-6,0q�6   .+�5   
   �� ���	#FFF4F4F5$
��Left=�6%   $	��671��6@   
���   �$	��Top=�6\   $	��175��6t   $��	-10,0,0,0q�6�   �� 5   
   $
��Left=�6   $��95��6+   $��26,0,0,0q�67   $	��Top=�6I   $	��100��6a   $
��Fill=�6m   $!��Icons/Format-Bullets-01.png��6|    J� OpacityMask
 �   � J� RenderTransformOrigin$ 0.55,0.3&�6�   �� 5   
   $��	Contratos��6   $��Center=�6$   $��2,108,1133,0q�6A   $	��Top=�6W   ���	#FFA8A2A2$	��157��6�   
���   �$��Center=�6�   $��20(�6�   $��Medium"�6�   
���   �6�   �� 5   
   $
��Left=�6   $	��104��6+   $��	0,170,0,0q�68   $	��Top=�6K   $	��160��6c   $
��Fill=�6o   $��Images/logo.png��6~   �� 5   
   $��Eventos��6   $��Center=�6"   $��26,340,1166,0q�6?   $	��Top=�6V   $	��100��6�   
���   �$��Center=�6�   $��18(�6�   $��SemiBold"�6�   ��6n   Z� 6�   �� 5   
   $��Empresariales��6   $��Center=�6(   $��-1,411,1133,0q�6E   $	��Top=�6\   $	��160��6�   
���   �$��Center=�6�   $��18(�6�   $��SemiBold"�6�   ��6t   Z� 6�   �� 5   
   -   5        $
��Menu��5      
�������$
��Left=�63   $��
26,610,0,0q�6N   $	��Top=�6b   $	��110��6z   ���	#FFB82020$��Medium"�6�   $��18(�6�   
���   �6�   �� 5   
   
���   �$
��Left=�6!   $	��100��6<   $��
160,28,0,0q�6I   
���   �$	��Top=�6l   $
��1130��6�   �� 5   
   $��LISTADO DE CONTRATOS��6   $��Center=�6/   $��157,0,0,638q�6L   $��Bottom=�6a   
�������$
��1135��6�   
���   �$��Center=�6�   $��20(�6�   $
��Bold"�6�   $��33��6�   �� 5   
   $'��!Filtrar busqueda de clientes por:��6   $��Center=�6<   $��165,33,847,0q�6Y   $	��Top=�6o   $	��280��6�   
�������$��Center=�6�   $��18(�6�   ��6�   Z� 6�   �� 5   
   $��N° Contrato��6   $��Center=�6&   $��170,76,1011.333,0q�6C   $	��Top=�6^   $	��110��6�   
�������$��Center=�6�   $��18(�6�   $ 0.638,0.765&�6�   ��6v   Z� 6�   �� 5   
   -   5         ��Name$ txtContratoFiltrar��5      $
��Left=�6.   $��35��6I   *   $MahApps.Metro.Controls.TextBoxHelper   ClearTextButton .    	Watermark$  ��5      $��
285,75,0,0q�5       �� TextWrapping$
 Wrap=�6&   $	��Top=�6:   $	��180��6R   $��18(�5       ���	#FFF3EBEB$ 0.286,0.571&�68   .��6\   z�5!      V� 5"      .~�5#      �� �� 5$       �� AngleX$ 2.088Q�6$   �� 5%      T� 5&      	 T� X$	 0.651Q�6)   5'      .��5(   '   5)      �� 5*   
   $��Rut Cliente��6   $��Center=�6&   $��488,76,706.333,0q�6C   $	��Top=�6]   $��97��6�   
�������$��Center=�6�   $��18(�6�   ��6u   Z� 6�   �� 5+   
   -   5        $ txtRutFiltrar��5+      $
��Left=�6)   $��35��6D    . $  ��5,      $��
590,75,0,0q�5-      $
 Wrap=�6!   $	��Top=�65   $	��176��6M   $��18(�5.      ���	#FFF3EBEB$ 0.466,0.375&�62   .��6V   z�5/      V� 50      .~�51      �� �� 52      $ 2.088Q�6$   �� 53      T� 54      $	 0.401Q�6)   55      .��56   '   57      �� 58   
   $��Nombre Evento��6   $��Center=�6(   $��785,74,371.333,0q�6E   $	��Top=�6_   $	��135��6�   
�������$��Center=�6�   $��18(�6�   $ 0.313,0.324&�6�   ��6w   Z� 6�   �� 59   
   -   5        $ txtTipoFiltrar��59      $
��Left=�6+   $��35��6F    . $  ��5:      $��
935,75,0,0q�5;      $
 Wrap=�6!   $	��Top=�65   $	��215��6M   $��18(�5<      ���	#FFF3EBEB$ 0.484,0&�62   .��6R   z�5=      V� 5>      .~�5?      �� �� 5@      $ 2.088Q�6$   �� 5A      T� 5B      $	 -0.077Q�6)   5C      .��5D   '   5E      �� 5F   
   -   5        $��Filtrar��5F      
���   �$
��Left=�66   $��1181,72,0,0q�6Q   $	��Top=�6f   $��82��6~   ���	#FFF3EC51$��SemiBold"�6�   $��18(�6�   $ 0.064,0.605&�6�   �� 5G   
   $��186,133,28,0q�6   $	��Top=�6&   
�������$��2��6Q   &   System.Windows.Controls.DataGrid  5J   
   -   5        
  Name$
 GridContratos��5J   %      
IsReadOnly . $
��Left=�6<   $	��435��6W   $��185,155,0,0q�6d   $	��Top=�6y   $
��1078��6�   �� 5M   
   -   5         ��Name$ btnSelecionarContrato��5M      $��SELECCIONAR CONTRATO��60   
�������$
��Left=�6b   $��995,610,0,0q�6}   $	��Top=�6�   $	��235��6�   ���	#FF27B820$��SemiBold"�6�   $��18(�6�   $ 0.179,0.395&�6  �� 5N   
   -   5        $ btnCargarCliente��5N      $��lISTADO DE CLIENTES��6+   
�������$
��Left=�6\   $��735,610,0,0q�6w   $	��Top=�6�   $	��230��6�   ���	#FF27B820$��SemiBold"�6�   $��18(�6�   $ 0.5,0.5&�6  5P      5Q      