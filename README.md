# Oyun Programlama Projesi

## 🎯 Oynanış (Gameplay)
Oyun bir **Birinci Şahıs Nişancı (FPS)** oyunudur. Haritanın rastgele belirlenen noktalarından düşmanlar (Hellish Sheriff) doğar ve oyuncuyu bulmak için tamamen kendi yazdığımız yapay zeka tarafından yönetilirler. Oyuncunun amacı, üzerine gelen düşmanları vurarak hayatta kalmaktır. Oyun içerisinde düşmanlardan saklanabileceğiniz engeller ve girebileceğiniz binalar bulunmaktadır.

### 🤖 Yapay Zeka ve Öne Çıkan Özellikler
*   **Sıfırdan C# ile Yazılmış Sinir Ağı (Neural Network):** Düşmanlar, kural (if-else) tabanlı bir sistemle değil; tamamen kendi yazdığımız Sinir Ağı modelinin ağırlıklarına göre karar verirler.
*   **3 Farklı AI Aksiyonu:** Düşmanlar sinir ağı kararlarına göre; **Ateş Etme (Shoot)**, **Oyuncuyu Kovalamaca (Chase)** ve **Yanlara Kaçma/Dodge (Dodge)** aksiyonlarını uygular.
*   **Eğitilebilir Model (Untrained vs Pre-Trained):** Ana menüden "Eğitilmemiş (Untrained)" veya "Ön Eğitimli (Pre-Trained)" yapay zeka ile oyuna başlayabilirsiniz. Ön eğitimli model, daha önce eğitilmiş ağırlıkları JSON dosyasından yükler.
*   **3D Ses ve Animasyonlar:** Düşmanların silah sesleri mesafeye göre 3D (Spatial Audio) olarak duyulur. Yürüme, koşma ve ateş etme animasyonları Avatar Mask kullanılarak katmanlı (Layered) bir şekilde sorunsuz çalışır.
*   **Oyun İçi Menüler:** Sorunsuz çalışan Ana Menü (Untrained/Pre-Trained seçimi) ve Durdurma (Pause) Menüsü.
*   **Sürekli Entegrasyon (CI):** Proje, GitHub Actions (GameCI) kullanılarak her push işleminde otomatik test edilir ve derlenir.

## ⌨️ Kontroller (Controls)
*   **W, A, S, D:** Hareket Etme
*   **Fare (Mouse):** Etrafa Bakma
*   **Sol Tık (Left Click):** Ateş Etme
*   **Space (Boşluk):** Zıplama
*   **C:** Çömelme / Kalkma
*   **ESC:** Oyunu Durdurma (Pause) Menüsü

## 🖼️ Oyun İçi Görseller

<img width="941" height="432" alt="image" src="https://github.com/user-attachments/assets/54831e1b-3559-4cd2-b96c-2f6ced5f41fe" />
<img width="945" height="433" alt="image" src="https://github.com/user-attachments/assets/2dc8c49b-5ed7-4351-9457-8f5b604bc0f0" />


## 🔗 Bağlantılar (Links)
*   **🎥 Oynanış Videosu (2-5 Dakika):** https://youtu.be/yqWwWuoIOdg
*   **🕹️ Oynanabilir Oyun (Itch.io):** [Itch.io Sayfası Linki Buraya Gelecek](BURAYA_ITCH_IO_LINKI_EKLE)

---
*Bu proje Bursa Teknik Üniversitesi, Bilgisayar Mühendisliği BLM0364 Oyun Programlama dersi kapsamında geliştirilmiştir.*
MOHAMAD ALI TOFAILY - 23360859810
Yiğit Berat Kılıç - 24360859028
