import { useEffect, useState, useCallback } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function AdminPanel() {
  const [categories, setCategories] = useState([]);
  const [newCategoryName, setNewCategoryName] = useState('');
  const [loading, setLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate();

  // 1. Mevcut Kategorileri Çek
  const fetchCategories = useCallback(async () => {
    try {
      const token = localStorage.getItem("token");
      if (!token) return navigate('/');

      const response = await axios.post("https://localhost:44339/api/Categories/getAllCategories", {}, {
        headers: { Authorization: `Bearer ${token}` }
      });
      setCategories(response.data || []);
      setLoading(false);
    } catch (error) {
      console.error("Kategoriler çekilemedi:", error);
      setLoading(false);
    }
  }, [navigate]);

  useEffect(() => {
    fetchCategories();
  }, [fetchCategories]);

  // 2. Yeni Kategori Ekle
  const handleAddCategory = async (e) => {
    e.preventDefault();
    if (!newCategoryName.trim()) return;
    
    setIsSubmitting(true);

    try {
      const token = localStorage.getItem("token");
      
      // C# tarafındaki CategoryCreateDto beklentisine göre sadece 'name' gönderiyoruz
      const payload = {
        name: newCategoryName
      };

      await axios.post("https://localhost:44339/api/Categories/createCategory", payload, {
        headers: { Authorization: `Bearer ${token}` }
      });

      setNewCategoryName('');
      alert("Kategori başarıyla eklendi! 🎯");
      
      // Listeyi anında güncelle
      fetchCategories();

    } catch (error) {
      console.error("Kategori ekleme hatası:", error.response?.data || error);
      alert("Hata: Kategori eklenemedi.");
    } finally {
      setIsSubmitting(false);
    }
  };

  if (loading) return <div style={{ padding: '50px', textAlign: 'center', fontSize: '1.2rem' }}>Kategoriler yükleniyor... ⏳</div>;

  return (
    <div style={{ padding: '30px', fontFamily: 'sans-serif', backgroundColor: '#f4f7f6', minHeight: '100vh' }}>
      
      {/* ÜST BİLGİ */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '30px' }}>
        <h2 style={{ color: '#2c3e50', margin: 0 }}>⚙️ Admin Paneli - Kategori Yönetimi</h2>
        <button onClick={() => navigate('/dashboard')} style={{ backgroundColor: '#007bff', color: 'white', border: 'none', padding: '10px 20px', borderRadius: '8px', cursor: 'pointer', fontWeight: 'bold' }}>
          ⬅️ Özet'e Dön
        </button>
      </div>

      <div style={{ display: 'flex', gap: '30px', flexWrap: 'wrap' }}>
        
        {/* SOL TARAF: MEVCUT KATEGORİLER LİSTESİ */}
        <div style={{ flex: '2', minWidth: '300px', background: 'white', padding: '30px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)' }}>
          <h3 style={{ color: '#2c3e50', borderBottom: '2px solid #f0f0f0', paddingBottom: '15px', marginTop: 0 }}>Sistemdeki Kategoriler 📋</h3>
          
          {categories.length > 0 ? (
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(150px, 1fr))', gap: '15px', marginTop: '20px' }}>
              {categories.map((cat, index) => (
                <div key={index} style={{ backgroundColor: '#f8f9fa', padding: '15px', borderRadius: '8px', border: '1px solid #ddd', textAlign: 'center', fontWeight: 'bold', color: '#555' }}>
                  🏷️ {cat.name}
                </div>
              ))}
            </div>
          ) : (
            <p style={{ color: '#888', fontStyle: 'italic' }}>Hiç kategori bulunamadı.</p>
          )}
        </div>

        {/* SAĞ TARAF: YENİ KATEGORİ EKLEME FORMU */}
        <div style={{ flex: '1', minWidth: '300px', background: 'white', padding: '30px', borderRadius: '15px', boxShadow: '0 4px 15px rgba(0,0,0,0.05)', height: 'fit-content' }}>
          <h3 style={{ color: '#2c3e50', borderBottom: '2px solid #f0f0f0', paddingBottom: '15px', marginTop: 0 }}>Yeni Kategori Ekle ➕</h3>
          
          <form onSubmit={handleAddCategory} style={{ display: 'flex', flexDirection: 'column', gap: '15px', marginTop: '20px' }}>
            <div>
              <label style={{ fontSize: '0.9rem', color: '#555', fontWeight: 'bold' }}>Kategori Adı</label>
              <input 
                type="text" 
                value={newCategoryName} 
                onChange={(e) => setNewCategoryName(e.target.value)} 
                required 
                placeholder="Örn: Araç Masrafları, Kripto..." 
                style={{ width: '100%', padding: '10px', marginTop: '5px', borderRadius: '8px', border: '1px solid #ccc' }} 
              />
            </div>

            <button type="submit" disabled={isSubmitting} style={{ padding: '12px', backgroundColor: '#2c3e50', color: 'white', border: 'none', borderRadius: '8px', cursor: isSubmitting ? 'not-allowed' : 'pointer', fontWeight: 'bold', fontSize: '1rem', marginTop: '10px' }}>
              {isSubmitting ? 'Ekleniyor...' : 'Kategoriyi Kaydet'}
            </button>
          </form>
        </div>

      </div>
    </div>
  );
}

export default AdminPanel;